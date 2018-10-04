<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Home</title>

    <!-- Bootstrap & CSS -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/css/styleHome.css" rel="stylesheet">
    <link href="assets/css/datatables.min.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->

    <link rel="apple-touch-icon" sizes="57x57" href="assets/ico/apple-icon-57x57.png">
    <link rel="apple-touch-icon" sizes="60x60" href="assets/ico/apple-icon-60x60.png">
    <link rel="apple-touch-icon" sizes="72x72" href="assets/ico/apple-icon-72x72.png">
    <link rel="apple-touch-icon" sizes="76x76" href="assets/ico/apple-icon-76x76.png">
    <link rel="apple-touch-icon" sizes="114x114" href="assets/ico/apple-icon-114x114.png">
    <link rel="apple-touch-icon" sizes="120x120" href="assets/ico/apple-icon-120x120.png">
    <link rel="apple-touch-icon" sizes="144x144" href="assets/ico/apple-icon-144x144.png">
    <link rel="apple-touch-icon" sizes="152x152" href="assets/ico/apple-icon-152x152.png">
    <link rel="apple-touch-icon" sizes="180x180" href="assets/ico/apple-icon-180x180.png">
    <link rel="icon" type="image/png" sizes="192x192" href="assets/ico/android-icon-192x192.png">
    <link rel="icon" type="image/png" sizes="32x32" href="assets/ico/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="96x96" href="assets/ico/favicon-96x96.png">
    <link rel="icon" type="image/png" sizes="16x16" href="assets/ico/favicon-16x16.png">
    <link rel="manifest" href="assets/ico/manifest.json">
    <meta name="msapplication-TileColor" content="#ffffff">
    <meta name="msapplication-TileImage" content="assets/ico/ms-icon-144x144.png">
    <meta name="theme-color" content="#ffffff">
</head>

<body>
<div class="container-fluid">
    <div class="row">
        <div class="col-md-7">
            <h2>
                <b id="welkom" class="col-md-offset-5">Welkom </b>
            </h2>
        </div>
        <div class="col-md-5">
            <a id="afmelden" class="btn btn-link">Afmelden<img
                        id="afmeldenPng"
                        alt="afmelden"
                        src="assets/img/afmelden.png"/></a>
        </div>
    </div>
</div>
<nav class="navbar navbar-inverse navbar-static-top" role="navigation">
    <div class="container">
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse"
                    data-target="#bs-example-navbar-collapse-1">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a href="homescreenStudent.php" class="hidden-sm hidden-xs logo"><img src="assets/img/pxllogowitterand.png"></a>
        </div>

        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav">
                <li class="current"><a href="homescreenStudent.php">Home</a></li>
                <li><a href="overzichtStageopdrachtenStudent.php">Overzicht
                        stageopdrachten</a></li>
                <li><a href="GegevensBijwerkenStudent.php">Gegevens bijwerken</a></li>
            </ul>
        </div>
    </div>
</nav>
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <h3>
                Favoriete stageopdrachten
            </h3>
            <table id="table1" class="table table-bordered" style="width: 100%">
                <thead>
                <tr>
                    <th>
                        Id
                    </th>
                    <th>
                        Functie
                    </th>
                    <th>
                        Bedrijf
                    </th>
                    <th>
                        Plaats
                    </th>
                </tr>
                </thead>
                <tbody>

                </tbody>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h3>
                Mijn geselecteerde stageopdrachten
            </h3>
            <table id="table2" class="table table-bordered" style="width: 100%">
                <thead>
                <tr>
                    <th>
                        Id
                    </th>
                    <th>
                        Functie
                    </th>
                    <th>
                        Bedrijf
                    </th>
                    <th>
                        Plaats
                    </th>
                    <th>
                        Status
                    </th>
                </tr>
                </thead>
                <tbody>

                </tbody>
            </table>
        </div>
    </div>
</div>
<form role="form" action="algemeenOverzichtStageopdrachtStudent.php" method="post" id="id-form">
    <input type="hidden" id="id_opdracht" name="id_opdracht" value="#">
</form>

<script src="assets/js/jquery.min.js"></script>
<script src="assets/js/bootstrap.min.js"></script>
<script src="assets/js/scriptHome.js"></script>
<script src="assets/js/datatables.min.js"></script>
<script src="assets/js/dataTransfer.js"></script>
<script src="assets/js/initializeDatatables.js"></script>
<script src="assets/js/sessionTimeout.js"></script>
<script src="assets/js/apiConnection.js"></script>
<script>
    $(document).ready(function () {

        var contents = document.getElementById('welkom').innerHTML;
        contents += sessionStorage.getItem('username');
        document.getElementById('welkom').innerHTML = contents;

        $.ajax({
            url: BaseURL+"/Student/home/" + sessionStorage.getItem("userId"),
            type: "Get",
            dataType: "json",
            async: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
            },
            success: function (data) {
                for (var i = 0; i < data.Favorieten.length; i++) {
                    var id = data.Favorieten[i].Id;
                    var functie = data.Favorieten[i].Omgeving;
                    var bedrijf = data.Favorieten[i].Opdrachtgever.Bedrijfsnaam;
                    var plaats = data.Favorieten[i].Locatie;
                    $('#table1 tbody').append('<tr><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td></tr>');
                }
                if (data.GekozenStageOpdracht != null) {
                    var id = data.GekozenStageOpdracht.StageOpdracht.Id;
                    var functie = data.GekozenStageOpdracht.StageOpdracht.Omgeving;
                    var bedrijf = data.GekozenStageOpdracht.StageOpdracht.Opdrachtgever.Bedrijfsnaam;
                    var plaats = data.GekozenStageOpdracht.StageOpdracht.Locatie;
                    var status = data.GekozenStageOpdracht.StageStatus;
                    if (status === 0) {
                        status = "In behandeling";
                        $('#table2 tbody').append('<tr class="warning"><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td><td>' + status + '</td></tr>');
                    } else if (status === 1) {
                        status = "Goedgekeurd";
                        $('#table2 tbody').append('<tr class="success"><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td><td>' + status + '</td></tr>');
                    } else {
                        status = "Afgekeurd";
                        $('#table2 tbody').append('<tr class="danger"><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td><td>' + status + '</td></tr>');
                    }
                }
            },
            error: function (data, jqXHR, textStatus, errorThrown) {
                console.error("Error: Status: " + textStatus + " Message: " + errorThrown + data);
            }
        });
        initializeDatatable('table1');
        initializeDatatable('table2');
    });
</script>
</body>
</html>