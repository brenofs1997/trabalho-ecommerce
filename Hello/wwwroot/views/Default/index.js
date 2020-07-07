//padrao modulo para organizar  o codigo

let index = {


    btnEntrarOnClick: function ()
    {

        let nomeUsuario = document.getElementById("name").value;
        let senhaUsuario = document.getElementById("senha").value;

        let dados = {
            nome: nomeUsuario,
            senha: senhaUsuario
        }
       
        let json = JSON.stringify(dados);
        var config = {
            method: "POST",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include',
            body: JSON.stringify(dados) //serialização dos dados
        };

        fetch("Default/Logar", config)
            .then(function (dadosJson)
                {
                var obj = dadosJson.json();//deserialização
                return obj;
                })
            .then(function (dadosObj) {
                document.getElementById("divMsg").innerHTML = dadosObj.msg;
            })
            .catch(function () {
                document.getElementById("divMsg").innerHTML ="Deu errado";
            })

        ///alert("oi..." + v);
    }

}



