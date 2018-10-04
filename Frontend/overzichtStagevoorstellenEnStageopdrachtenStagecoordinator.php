<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Stageopdrachten</title>

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
        <div class="col-md-12">
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
            <a href="homescreenStagecoordinator.php" class="hidden-sm hidden-xs logo"><img src="assets/img/pxllogowitterand.png"></a>
        </div>

        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav">
                <li><a href="homescreenStagecoordinator.php">Home</a></li>
                <li class="current"><a href="overzichtStagevoorstellenEnStageopdrachtenStagecoordinator.php">Overzicht stageopdrachten</a></li>
                <li><a href="GegevensBijwerkenStagecoordinator.php">Gegevens bijwerken</a></li>
            </ul>
        </div>
    </div>
</nav>
<div class="container-fluid">
    <div class="row">
        <div class="col-md-12">
            <h3>
                Overzicht stageopdrachten
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
                    <th class="visible-md visible-lg">
                        Status
                    </th>
                    <th class="visible-md visible-lg">
                        Reviewer
                    </th>
                </tr>
                </thead>
                <tbody>

                </tbody>
            </table>
        </div>
    </div>
</div>
<form role="form" action="algemeenOverzichtStageOpdrachtStageCoordinator.php" method="post" id="id-form">
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

        $.ajax({
            url: BaseURL+"/stagevoorstellen/",
            type: "GET",
            dataType: "json",
            async: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
            },
            success: function (data) {
                var len = data.length;
                for (var i = 0; i < len; i++) {
                    var id = data[i].Stageopdracht.Id;
                    var functie = data[i].Stageopdracht.Omgeving;
                    var bedrijf = data[i].Stageopdracht.Opdrachtgever.Bedrijfsnaam;
                    var plaats = data[i].Stageopdracht.Locatie;
                    var status = data[i].Stageopdracht.Status;
                    var reviewer = "Nog niet toegekend";

                    if (data[i].ReviewLectorId !== null) {
                        $.ajax({
                            url: BaseURL+'/Lector/' + data[i].ReviewLectorId,
                            type: 'GET',
                            contentType: 'application/json; charset=utf-8',
                            dataType: 'json',
                            async: false,
                            beforeSend: function (xhr) {
                                xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
                            },
                            success: function (data) {
                                reviewer = data.Voornaam + " " + data.Achternaam;
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                console.error("Error: Status: " + textStatus + " Message: " + errorThrown);
                            }
                        });
                    } else if (data[i].Stageopdracht.Status === 2 || data[i].Stageopdracht.Status === 3) {
                        reviewer = "Marijke Willems";
                    }

                    if (status === 0) {
                        status = "Nieuw";
                        $('#table1 tbody').append('<tr><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td><td class="visible-md visible-lg">' + status + '</td><td class="visible-md visible-lg">' + reviewer + '</td></tr>')
                    } else if (status === 1) {
                        status = "In behandeling";
                        $('#table1 tbody').append('<tr class="warning"><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td><td class="visible-md visible-lg">' + status + '</td><td class="visible-md visible-lg">' + reviewer + '</td></tr>')
                    } else if (status === 2) {
                        status = "Goedgekeurd";
                        $('#table1 tbody').append('<tr class="success"><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td><td class="visible-md visible-lg">' + status + '</td><td class="visible-md visible-lg">' + reviewer + '</td></tr>')
                    } else if (status === 3) {
                        status = "Afgekeurd";
                        $('#table1 tbody').append('<tr class="danger"><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td><td class="visible-md visible-lg">' + status + '</td><td class="visible-md visible-lg">' + reviewer + '</td></tr>')
                    } else if (status === 4) {
                        status = "Goedgekeurd, wachten op oordeel stagecoordinator";
                        $('#table1 tbody').append('<tr class="info"><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td><td class="visible-md visible-lg">' + status + '</td><td class="visible-md visible-lg">' + reviewer + '</td></tr>')
                    } else if (status === 5) {
                        status = "Afgekeurd, wachten op oordeel stagecoordinator";
                        $('#table1 tbody').append('<tr class="info"><td>' + id + '</td><td>' + functie + '</td><td>' + bedrijf + '</td><td>' + plaats + '</td><td class="visible-md visible-lg">' + status + '</td><td class="visible-md visible-lg">' + reviewer + '</td></tr>')
                    }
                }
            },
            error: function (data, jqXHR, textStatus, errorThrown) {
                console.error("Error: Status: " + textStatus + " Message: " + errorThrown + data);
            }
        });
        initializeDatatable('table1');
    });
</script>
</body>
</html>