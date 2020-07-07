var index = {
    btnCadastrar: function () {

        var descricao = document.getElementById("descricao").value;
        var categoria = document.querySelector("#selCategoria").value;
        var valor = document.querySelector("#valor").value;
        var quantidade = document.querySelector("#quantidade").value;
        if (descricao.trim() == "") {

            document.getElementById("divMsg").innerHTML=
                "Informe o nome.";
        }
        else if (valor.trim() <= 0) {

            document.getElementById("divMsg").innerHTML =
                "Valor não informado corretamente";
        }
        else if (quantidade <= 0) {

            document.getElementById("divMsg").innerHTML =
                "Qtde. sem valor ou zerada";
        }
        else if (categoria.trim() == "") {

            document.getElementById("divMsg").innerHTML =
                "Categoria não informada.";
        }
        else {

            var dados = {
                descricao,
                categoria,
                quantidade,
                valor
            }

            var config = {
                method: "POST",
                headers: {
                    "Content-Type": "application/json; charset=utf-8"
                },
                credentials: 'include', //inclui cookies
                body: JSON.stringify(dados)  //serializa
            };

            fetch("/CadastrarProduto/Criar", config)
                .then(function (dadosJson) {
                    var obj = dadosJson.json(); //deserializando
                    return obj;
                })
                .then(function (dadosObj) {
                    if (!dadosObj.operacao) {
                        document.getElementById("divMsg").innerHTML = dadosObj.msg;
                    }
                    else {

                        //enviando a imagem
                        var id = dadosObj.id;

                        var fd = new FormData();
                        for (var i = 0; i < document.getElementById("foto").files.length;i++)
                        fd.append("foto"+"i", document.getElementById("foto").files[i]);

                        fd.append("id", id);

                        var configFD = {
                            method: "POST",
                            headers: {
                                "Accept": "application/json"
                            },
                            body: fd
                        }

                        fetch("/CadastrarProduto/Foto", configFD)
                            .then(function (dadosJson) {
                                var obj = dadosJson.json(); //deserializando
                                return obj;
                            })
                            .then(function (dadosObj) {

                                alert("Cadastro Efetuado com Sucesso");
                                window.location.href = "Index";
                            })
                            .catch(function () {
                                document.getElementById("divMsg").innerHTML = "deu erro na foto";
                                   
                            })
                    }
                    

                })
                .catch(function () {

                    document.getElementById("divMsg").innerHTML = dadosObj.msg;
                    document.getElementById("divMsg").className = "alert alert-danger";
                    document.getElementById("divMsg").style.marginTop = "15px";
                })

        }
    },

    buscarCategorias: function () {

        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui cookies
        };

        fetch("/CategoriaProduto/ObterCategorias", config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {

                var selCategoria = document.getElementById("selCategoria");
                var opts ="<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option value="${dadosObj[i].id}">${dadosObj[i].descricao}</option>`;                   
                    //opts += "<option value='" + dadosObj[i] + "'>" + dadosObj[i] + "</option>"
                }

                selCategoria.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })

    },
    buscarProduto: function (pt) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', //inclui 
        };

        fetch("/CategoriaProduto/ObterProdutos?pt=" + pt, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                var display = document.getElementById("contDIV").style.display;
                var selProduto = document.getElementById("selProduto");

                if (display == "none" && selProduto.innerHTML != "")
                    document.getElementById("contDIV").style.display = 'block';
                else
                    document.getElementById("contDIV").style.display = 'none';

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].descricao}</option>`;
                }

                selProduto.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    },
    ListarProduto: function (pt) {
        var config = {
            method: "GET",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            credentials: 'include', ///inclui 
        };

        fetch("/CategoriaProduto/listar?pt=" + pt, config)
            .then(function (dadosJson) {
                var obj = dadosJson.json(); //deserializando
                return obj;
            })
            .then(function (dadosObj) {
                var display = document.getElementById("contDIV").style.display;
                var selProduto = document.getElementById("selProduto");

                if (display == "none" && selProduto.innerHTML != "")
                    document.getElementById("contDIV").style.display = 'block';
                else
                    document.getElementById("contDIV").style.display = 'none';

                var opts = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++) {

                    opts += `<option 
                            value="${dadosObj[i].id}">
                            ${dadosObj[i].descricao}</option>`;
                }
                var divs = "<option value=''></option>";
                for (var i = 0; i < dadosObj.length; i++)
                {

                    divs += `     
                

                    <div class="col-lg-3 col-md-4 mb-4">
                        <div class="card h-100">
                           
                            <div id="carouselExampleControls${dadosObj[i].id}" class="carousel slide" data-ride="carousel">
                                     <div class="carousel-inner">`;
                    for (var j = 0; j < dadosObj[i].qtdeFoto; j++)
                    {
                        if (j == 0) {
                            divs += `
                            <div class="carousel-item active">

                            <img class="d-block w-100" src="/CadastrarProduto/ObterFotoS?id=${dadosObj[i].id}&pos=${j}" alt="${j}slide">
                                    </div> `;
                        }
                        else
                        {
                            divs += `
                                    <div class="carousel-item ">
                   
                                      <img class="d-block w-100" src="/CadastrarProduto/ObterFotoS?id=${dadosObj[i].id}&pos=${j}" alt="${j} slide">
                                    </div>   
                                    `;
                        }
                       
                    }
                    divs += `
                                  </div>
                                  <a class="carousel-control-prev" href="#carouselExampleControls${dadosObj[i].id}" role="button" data-slide="prev">
                                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span class="sr-only">Previous</span>
                                  </a>
                                  <a class="carousel-control-next" href="#carouselExampleControls${dadosObj[i].id}" role="button" data-slide="next">
                                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span class="sr-only">Next</span>
                                  </a>
                                </div>                                  


                            <div class="card-body">
                                <h4 class="card-title">
                                    <a href="#">${dadosObj[i].descricao}</a>
                                </h4>
                                <h5>R$:${dadosObj[i].valor}</h5>
                                <p class="card-text">Lorem ipsum dolor sit amet, consectetur adipisicing elit. Amet numquam aspernatur!</p>
                            </div>
                            <div class="card-footer">
                                <small class="text-muted">&#9733; &#9733; &#9733; &#9733; &#9734;</small>
                            </div>
                        </div>
                    </div>

                  

                  `;
                }
                contDIV.innerHTML = divs;
                selProduto.innerHTML = opts;

            })
            .catch(function () {

                document.getElementById("divMsg").innerHTML = "deu erro";
            })
    }
}
index.buscarCategorias();