

const form = document.getElementById("forms")

form.addEventListener("submit", (event) => {

    event.preventDefault()

    const USERNAME = document.getElementById("cuenta").value
    const USERS_PASS = document.getElementById("pass").value
    const array = { USERNAME, USERS_PASS }
    const JsonValue = JSON.stringify(array)

    fetch("https://localhost:7057/api/users/login", {
        method: 'post',
        body: JsonValue,
        headers: {
            'Content-Type': 'application/json'
        }
    }).then((res) => {
        if (res.ok) {
            
            localStorage.setItem("username", USERNAME );
            const jsonDataString = JSON.stringify(res);
            localStorage.setItem("values", jsonDataString);
            return res.json();
        } else {
            throw new Error(`Error en la solicitud: ${res.status}`);
        }
    })
    .then((data) => {
        if (!data || (Array.isArray(data) && data.length === 0)) {

            alert("Cuenta Invalida")
           // console.warn('La respuesta JSON está vacía o no contiene datos.');
            
        }else{

            var jsonResult = JSON.stringify(data)
            

            localStorage.setItem("values", jsonResult);
            
            
            var storedJson = localStorage.getItem("values");
            var parsedJson = JSON.parse(storedJson);

            window.location.href = "../views/loggedUsers.html";
            console.log(parsedJson);
            return data;
        }
        

    })
    .catch(() => {
       
        console.log({ error: "Cuenta invalida" });
    });
    
    
});
