
const pantalla = document.getElementById("pantalla_id")
const boton = document.querySelectorAll(".butn")

let globalId;
let globalprimerNumero_;
let globalsegundoNumero_;
let globaloperador_;
var user_name_welcome = document.getElementById("user_name")
user_name_welcome.textContent = localStorage.getItem("username")


boton.forEach(element => {


    element.addEventListener("click",()=>{


        if(element.id ==="c"){

            pantalla.textContent = "0"
            return
        }

        if(element.id === "borrar"){

            if(pantalla.textContent.length === 1 || pantalla.textContent === "Invalido!"){

                pantalla.textContent = "0"

            }else{

                pantalla.textContent = pantalla.textContent.slice(0,-1)

            }
            
            return
        }


        if(element.id === "igual"){

            try {
                var contenidoPantalla = pantalla.textContent;
                var resultado = eval(pantalla.textContent);
                var resultadoFormateado = resultado.toFixed(4);
                pantalla.textContent = resultadoFormateado
                var elementos = contenidoPantalla.match(/([+-]?\d*\.?\d+)([+\-*/])([+-]?\d*\.?\d+)/);

                var primerNumero = elementos[1];
                var operatioN_SIMBOL = elementos[2];
                var segundoNumero = elementos[3];
                
                var firsT_DIGITED_NUMBER = parseFloat(primerNumero)
                var seconD_DIGITED_NUMBER = parseFloat(segundoNumero)

                console.log("Primer número:", primerNumero);
                console.log("Símbolo:", operatioN_SIMBOL);
                console.log("Segundo número:", segundoNumero);

                var arrays = {firsT_DIGITED_NUMBER,seconD_DIGITED_NUMBER,operatioN_SIMBOL}
                var JsonArrays = JSON.stringify(arrays)
                
                var data = localStorage.getItem("values")
                var dataArray = JSON.parse(data)
                var finalValue = dataArray[0];
                var id_u= finalValue.userS_ID;

                console.log("id usuario:",finalValue.userS_ID)
                console.log("username:",finalValue.username)
                console.log("firstname:",finalValue.firstname)
                console.log("lstname:",finalValue.lastname)
                console.log("pass",finalValue.userS_PASS)
                console.log("data:",data)

                

                fetch(`https://localhost:7057/api/calculator/${id_u}`,{

                    method:'post',
                    body:JsonArrays,
                    headers:{

                        'Content-Type':'application/json'
                    }
                }).then(()=>{
                    

                    getOperations()
                    return console.log("Operacion guardada")
                }).catch(()=> console.error({error:"Operacion denegada"}))

            } catch (error) {
                
                pantalla.textContent="Invalido!"
            }
            return
        }



        const currentButton = element.textContent

        if(pantalla.textContent === "0" || pantalla.textContent === "Invalido!"){

            pantalla.textContent = currentButton;
        }else{

            pantalla.textContent += currentButton;
        }
        
    })
});



function getOperations() {

    var data = localStorage.getItem("values");
    var dataArray = JSON.parse(data);
    var finalValue = dataArray[0];
    var id_ = finalValue.userS_ID;

    localStorage.setItem("userID",id_)
    localStorage.setItem("username",finalValue.username)
    localStorage.setItem("firstname",finalValue.firstname)
    localStorage.setItem("lastname",finalValue.lastname)
    localStorage.setItem("password",finalValue.userS_PASS)


    fetch(`https://localhost:7057/api/calculator/${id_}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
    })
        .then((response) => response.json())
        .then((data) => {

         
            const tableBody = document.getElementById("t_body");
            tableBody.innerHTML = "";

         
            data.forEach((element) => {
                const row = document.createElement("tr");

                const id = document.createElement("td");
                id.textContent = element.id;
                row.appendChild(id);

                const primerNumero = document.createElement("td");
                primerNumero.textContent = element.firsT_DIGITED_NUMBER;
                row.appendChild(primerNumero);

                const segundoNumero = document.createElement("td");
                segundoNumero.textContent = element.seconD_DIGITED_NUMBER;
                row.appendChild(segundoNumero);

                const operador = document.createElement("td");
                operador.textContent = element.operatioN_SIMBOL;
                row.appendChild(operador);

                const resultado = document.createElement("td");
                resultado.textContent = element.result;
                row.appendChild(resultado);

                const fecha = document.createElement("td");
                fecha.textContent = element.dates;
                row.appendChild(fecha);

                const espacio1 = document.createElement("span");
                espacio1.textContent = " ";
                row.appendChild(espacio1);

                const opciones = document.createElement("button");
                opciones.textContent = "Editar";
                opciones.className = "btn btn-primary";

                opciones.addEventListener("click", ()=> {

                    var nuevosNumeros = pantalla.textContent
                    var nuevosResultados = nuevosNumeros.match(/([+-]?\d*\.?\d+)([+\-*/])([+-]?\d*\.?\d+)/);
                    
                    var primerNumero_ = nuevosResultados[1];
                    var operatioN_SIMBOL = nuevosResultados[2];
                    var segundoNumero_ = nuevosResultados[3];

                    console.log("nuevos numeros:",nuevosNumeros)
                    var firsT_DIGITED_NUMBER = parseFloat(primerNumero_)
                    var seconD_DIGITED_NUMBER = parseFloat(segundoNumero_)
                    
   
                    var jsonvar = {firsT_DIGITED_NUMBER,operatioN_SIMBOL,seconD_DIGITED_NUMBER}
                    var jsonBody = JSON.stringify(jsonvar)
                    fetch(`https://localhost:7057/api/calculator/${globalId}`,{

                        method:'put',
                        body:jsonBody,
                        headers:{

                            'Content-Type':'application/json'
                        }

                    }).then(()=>{
                        
                        pantalla.textContent="0";
                        getOperations();
                    }).catch(()=>console.error({error:"no se pudo actualizar la operacion"}))
                
                });

                row.appendChild(opciones);

                const espacio = document.createElement("span");
                espacio.textContent = " ";
                row.appendChild(espacio);

                const opciones_eliminar = document.createElement("button");
                opciones_eliminar.textContent = "Eliminar";
                opciones_eliminar.className = "btn btn-primary";

                opciones_eliminar.addEventListener("click", function () {
                    
                    fetch(`https://localhost:7057/api/calculator/${globalId}`,{

                    method:'delete',
                    headers:{
                        'Content-Type':'application/json'
                    }

                    }).then(()=>{

                        console.log("Registro eliminado stisfactoriamente")
                        pantalla.textContent="0"
                        getOperations()
                    }).catch(()=> console.error({error:"El registro no se pudo eliminar"}))
                });

                row.appendChild(opciones_eliminar);

                opciones_eliminar.style.margin = "5px";
                row.appendChild(opciones_eliminar);

                tableBody.appendChild(row);
            });
        })
        .catch((error) => console.error("Error al obtener operaciones:", error));
}




document.addEventListener("DOMContentLoaded",()=>{

    getOperations()

    const tableBody = document.getElementById("t_body");
    tableBody.addEventListener("click", (event) => {
        const targetCell = event.target.closest("td");
        if (targetCell) {
           
            handleCellClick(targetCell);
        }
    });
})



function handleCellClick(cell) {
 
    globalId = cell.parentNode.querySelector("td:nth-child(1)").textContent;
    globalprimerNumero_ = cell.parentNode.querySelector("td:nth-child(2)").textContent;
    globalsegundoNumero_ = cell.parentNode.querySelector("td:nth-child(3)").textContent;
    globaloperador_ = cell.parentNode.querySelector("td:nth-child(4)").textContent;
    var resultado_ = cell.parentNode.querySelector("td:nth-child(5)").textContent;
    var fecha_ = cell.parentNode.querySelector("td:nth-child(6)").textContent;

    
    console.log("ID:", globalId);
    console.log("Primer Número:", globalprimerNumero_);
    console.log("Segundo Número:", globalsegundoNumero_);
    console.log("Operador:", globaloperador_);
    console.log("Resultado:", resultado_);
    console.log("Fecha:", fecha_);

    var edit_result = `${globalprimerNumero_}${globaloperador_}${globalsegundoNumero_}`

    pantalla.textContent = edit_result


}
