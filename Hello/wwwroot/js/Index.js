var Index2 = {

    btnCadastrar: function () {

        var nomeUsuario = document.getElementById("usuario").value;
        var senha = document.querySelector("#senha").value;
        var senhaConf = document.querySelector("#senhaConf").value;
        var divM = document.getElementById("erros");
        if (nomeUsuario.trim() == "") {

            divM.innerHTML =
                "Informe o nome.";
        }
        else if (senha.trim() == "" || senhaConf.trim() == "") {

            document.getElementById("erros").innerHTML =
                "As senhas não informadas.";
        }
        else if (senha != senhaConf) {

            document.getElementById("erros").innerHTML =
                "As senhas não conferem.";
        }
        else if (document.getElementById("foto").files.length == 0) {

            document.getElementById("erros").innerHTML =
                "Foto não selecionada.";
        }
        else {

            var dados = {
                nomeUsuario,
                senha,
                senhaConf
            }

            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(dados)  //serializa
            };

            fetch("CadastrarUsuario/Criar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {

                    window.location.href = "Default";
                    if (!dadosObj.operacao) { document.getElementById("erros").innerHTML = dadosObj.msg; }
                    else {
                        var id = dadosObj.id;
                        var fd = new FormData();
                        fd.append("foto", document.getElementById("foto").files[0]);
                        fd.append("id", id);
                        var configFD =
                        {
                            method: "POST",
                            headers: { "Accept": "application/json" },
                            body: fd

                        }
                        fetch("CadastrarUsuario/Foto", configFD)
                            .then(function (dadosObj) {
                                window.location.href = "Default";
                            })
                            .catch(function () {

                                document.getElementById("erros").innerHTML = "deu erro foto";
                            })
                    }
                })
                .catch(function () {

                    document.getElementById("erros").innerHTML ="deu erro ";
                })

        }
    },

    buscarEstados: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/Cidade/ObterEstados", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selUF = document.getElementById("selUF");
                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i]}">
                            ${dadosObj[i]}</option>`;
                    //opts += "<option value='" + dadosObj[i] + "'>" + dadosObj[i] + "</option>"
                }

                selUF.innerHTML = opts;

            })
            .catch(function () {
                var log = document.getElementById("erros");
                log.innerHTML = "deu erro";
            })

    },

    buscarCidades: function (uf) {


        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui 
        };

        fetch("Cidade/ObterCidades?uf=" + uf, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selCidade = document.getElementById("selCidade");
                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].nome}</option>`;
                }

                selCidade.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    }
}

//iniciando a página;
Index.buscarEstados();