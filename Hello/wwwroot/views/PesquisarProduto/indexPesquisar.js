var indexPesquisar = {

    excluir: function (id) {

        if (!confirm("Deseja excluír?")) {
            return;
        }

        var config = {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/CadastrarProduto/Excluir?id=" + id, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                if (dadosObj.operacao) {
                    var tr = document.querySelector("tr[data-id='" + id + "']");

                    tr.parentNode.removeChild(tr);
                }

            })
            .catch(function () {
                alert("Deu erro.")
            });



    },


    btnPesquisarOnClick: function () {

        document.getElementById("tbProdutos").style.display = "table";

        var tbodyUsuarios = document.getElementById("tbodyProdutos");
        tbodyUsuarios.innerHTML = `<tr><td colspan="4"><img src=\"/img/loader.gif"\ /</td></tr>`
        document.getElementById("btnPesquisar").disabled = "disabled";

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        var nome = encodeURIComponent(document.getElementById("produto").value);

        fetch("/PesquisarProduto/Pesquisar?nome=" + nome, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var linhas = "";
                for (var i = 0; i < dadosObj.length; i++) {

                    var template =
                        `<tr data-id="${dadosObj[i].id}">
                            <td>${dadosObj[i].id}</td>
                            <td>${dadosObj[i].descricao}</td>
                            <td>${dadosObj[i].valor}</td>
                            <td><a href="javascript:void" onclick="indexPesquisar.excluir(${dadosObj[i].id})">excluir</a></td>
                         </tr>`
                    linhas += template;
                }

                if (linhas == "") {

                    linhas = `<tr><td colspan="3">Sem resultado.</td></tr>`
                }

                tbodyUsuarios.innerHTML = linhas;
            })
            .catch(function () {
                tbodyUsuarios.innerHTML = `<tr><td colspan="3">Deu erro...</td></tr>`
            })
            .finally(function () {

                document.getElementById("btnPesquisar").disabled = "";
            });


    }

}