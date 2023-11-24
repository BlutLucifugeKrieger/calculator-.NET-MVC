using CalculadoraNET_JuanCastro.ControllerUtils;
using CalculadoraNET_JuanCastro.Models;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraNET_JuanCastro.Controllers
{
    [ApiController]
    [Route("api/calculator")]
    public class calculatorController
    {
        [HttpGet]
        public async Task<ActionResult<List<calculator>>> allOperations()
        {
            var List = new List<calculator>();
            
            try
            {
                calculatorUtils cal = new calculatorUtils();
                var result = await cal.getOperationsResults();
                return result;

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return List;
            }
            
        }

        [HttpPost("{id}")]
        public async Task<string> newOperation(int id,[FromBody] calculator cal, double operation)
        {
            try
            {
                calculatorUtils calu = new calculatorUtils();
                cal.USER_IDS = id;
                cal.DATES = DateTime.Now;
                calu.numberOperations(operation,cal);
                await calu.operations(cal);
                return "Operacion realizada exitosamente";

            }catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return "La operacion no se pudo realizar";
            }

        }


        [HttpPut("{id}")]
        public async Task<string> operationUpdate(int id,[FromBody] calculator cal , double operation)
        {

            try
            {

                calculatorUtils cals = new calculatorUtils();

                cal.ID = id;
                cal.DATES = DateTime.Now;
                cals.numberOperations(operation, cal);
                await cals.updateOperations(cal);
                return "Registro actualizado";

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return "No se pudo actualizar el registro";

            }
            


        }

        [HttpDelete("{id}")]
        public async Task<string> deleteOperation(int id)
        {

            try
            {

                calculatorUtils ui = new calculatorUtils();
                await ui.deleteOperations(id);
                return "Registro eliminado";

            }
            catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return "No se pudo eliminar el registro";

            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<calculator>>> getUsersOperations(int id)
        {
            try
            {
                calculatorUtils uis = new calculatorUtils();
                var result = await uis.getUsersOps(id);
                return result;
            }catch(Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return new List<calculator>();

            }
            
        }



    }
}
