using CalculadoraNET_JuanCastro.ControllerUtils;
using CalculadoraNET_JuanCastro.Models;
using Microsoft.AspNetCore.Mvc;



namespace CalculadoraNET_JuanCastro.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class usersController
    {

        [HttpGet]
        public async Task<ActionResult<List<users>>> allUsers()
        {
            var List = new List<users>();
            try
            {
                usersUtils user = new usersUtils();
                var result = await user.getAllUsers();
                return result;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return List;
            }
            
        }

        [HttpPost]
        public async Task<string> createUsers([FromBody] users value)
        {
            try
            {
                usersUtils user = new usersUtils();
                await user.createUser(value);
                return "Cuenta creada satisfactoriamente";

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return "La cuenta no se pudo crear";
            }
            

        }

        [HttpPut("{id}")]
        public async Task<string> updateUsers(int id, [FromBody] users value)
        {
            try
            {
                usersUtils user = new usersUtils();
                value.USERS_ID = id;
                await user.updateUser(value);
                return "Usuario Actualizado";
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return "El usuario no se pudo actualizar";
            }
          

        }

        [HttpDelete("{id}")]
        public async Task<string> deleteUsers(int id)
        {
            try
            {
                usersUtils user = new usersUtils();
                await user.deleteUser(id);
                return "Usuario eliminado";
            }
            catch(Exception ex)
            {

                await Console.Out.WriteLineAsync(ex.Message);
                return "El usuario no se pudo eliminar";
            }

            
        }

        
        [HttpPost("login")]
        public async Task<ActionResult<List<users>>> userLogin([FromBody] users value)
        {
            try
            {
                usersUtils u = new usersUtils();
                var result = await u.userlogging(value);
                return result;

            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new List<users>();
            }
            

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<users>>> getAnUserInfo(int id)
        {
            try
            {

                usersUtils u = new usersUtils();
                var result= await u.getAnUser(id);
                return result;
            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new List<users>();
            }
            

        }



    }
}
