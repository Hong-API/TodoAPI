using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using TodoAPI.Models;

namespace TodoAPI.Controllers
{
    [ApiController]
    public class TodoController : Controller
    {
        private readonly IConfiguration _config;
        private readonly SqlConnection _connection;

        public TodoController(IConfiguration config)
        {
            _config = config;
            _connection = new SqlConnection(_config.GetConnectionString("ApplicationSettingDb"));
        }

        // Get all data
        [HttpGet("/todo")]
        public async Task<ActionResult<List<Todo>>> getAll() 
        
        {
            var todos = await _connection.QueryAsync<Todo>("SELECT TOP (200) Id, Title,Description, Status,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy FROM HDB.dbo.Todos");
            return Ok(todos);
        }

        // Create new Todo
        [HttpPost("/todo")]
        public async Task<ActionResult> createTodo(Todo todo)
        {
            if (todo == null)
                return BadRequest();

            if (todo.Status != "pending" && todo.Status != "doing" && todo.Status != "done")
                return BadRequest("Status must be either 'pending', 'doing', or 'done'.");

            var response = await _connection.ExecuteAsync(@"INSERT INTO HDB.dbo.Todos (Title, Description, Status,createdOn,CreatedBy,ModifiedOn,ModifiedBy) 
                                                      VALUES (@Title, @Description, @Status, @createdOn,@CreatedBy,@ModifiedOn, @ModifiedBy)",
                                                              todo);
            if (response == 0)
                return StatusCode(500);

            return Ok("Create new task successfully"); 
        }

        // Get data by ID
        [HttpGet("/todo/{id}")]
        public async Task<ActionResult<List<Todo>>> getTodoById(int id)
        {
            if (id <= 0)
                return BadRequest();
            

            var todo = await _connection.QueryAsync<Todo>($"SELECT Id, Title,Description, Status,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy FROM HDB.dbo.Todos where Id = {id}");
            if(todo == null)
                return NotFound();
            return Ok(todo);
          
        }

        // Update data
        [HttpPut("/todo/{id}")]
        public async Task<ActionResult> upateTodo(int id, Todo todo)
        {
            if (todo.Status != "pending" && todo.Status != "doing" && todo.Status != "done")
                return BadRequest("Status must be either 'pending', 'doing', or 'done'.");

            await _connection.ExecuteAsync(@$"UPDATE HDB.dbo.Todos SET Title = @Title, Description = @Description, Status = @Status, ModifiedBy = @ModifiedBy,  ModifiedOn = @ModifiedOn WHERE Id = {id}",
                                                        new
                                                        {
                                                            todo.Title,
                                                            todo.Description,
                                                            todo.Status,
                                                            todo.ModifiedBy,
                                                            todo.ModifiedOn
                                                        });
             return Ok("Updated task successfully");
        }

        // Update Status 
        [HttpPut("/todo/{id}/status")]
        public async Task<ActionResult> updateStatus(int id, Todo todo)
        {
            if (todo.Status != "pending" && todo.Status != "doing" && todo.Status != "done")
                return BadRequest("Status must be either 'pending', 'doing', or 'done'.");

            await _connection.ExecuteAsync(@$"UPDATE HDB.dbo.Todos SET Status = @Status, ModifiedOn = @ModifiedOn, ModifiedBy = @ModifiedBy WHERE Id = {id}",
                                                        new
                                                        {
                                                            todo.Status,
                                                            todo.ModifiedOn,
                                                            todo.ModifiedBy
                                                        });
            return Ok("The status has been updated successfully");
        }

        // Delete Todo 
        [HttpDelete("/todo/{id}")]
        public async Task<ActionResult> deleteTodo(int id)
        {
            if (id <= 0)
                    return BadRequest();

     
            var rowsAffected = await _connection.ExecuteAsync($"DELETE FROM HDB.dbo.Todos WHERE Id = {id}");

            if (rowsAffected == 0)
                return NotFound(); 

            return Ok("Task has been deleted successfully"); 
        }
            
    }
}
