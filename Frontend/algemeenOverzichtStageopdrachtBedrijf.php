<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Details</title>

    <!-- Bootstrap & CSS -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet">
    <link href="assets/css/styleHome.css" rel="stylesheet">

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
            <a href="homescreenBedrijf.php" class="hidden-sm hidden-xs logo"><img src="assets/img/pxllogowitterand.png"></a>
        </div>
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav">
                <li><a href="homescreenBedrijf.php">Home</a></li>
                <li><a href="indienenstagevoorstelBedrijf.php">Stagevoorstel indienen</a></li>
                <li><a href="GegevensBijwerkenBedrijf.php">Persoonlijke gegevens</a></li>
            </ul>
        </div>
    </div>
</nav>
<div class="container">
    <div class="row">
        <div class="col-md-8">
            <h2 id="titel"></h2>
        </div>
        <div class="col-md-4">
            <p class="text-muted pull-right" id="time">Ingestuurd op: </p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <h3>
                Adresgegevens
            </h3>
            <address>
                <strong id="bedrijfsnaam"></strong><br/><span id="adres"></span><br/><span
                        id="gemeente"></span><br/><abbr title="Phone">P:</abbr><span
                        id="telefoonnummer"></span>
            </address>
        </div>
        <div class="col-md-4">
            <h3>
                Contactpersoon
            </h3>
            <strong>Naam: </strong>
            <span id="naamContactpersoon"></span><br/>
            <strong>Email: </strong>
            <span id="emailContactpersoon"></span><br/>
            <abbr title="Phone">P:</abbr>
            <span id="telefoonnummerContactpersoon"></span>
        </div>
        <div class="col-md-4">
            <h3>
                Bedrijfspromotor
            </h3>
            <strong>Naam: </strong>
            <span id="naamBedrijfspromotor"></span><br/>
            <strong>Email: </strong>
            <span id="emailBedrijfspromotor"></span><br/>
            <abbr title="Phone">P:</abbr>
            <span id="telefoonnummerBedrijfspromotor"></span>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h3>
                Technische informatie
            </h3>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <strong>Locatie (indien anders dan bedrijf): </strong>
            <span id="locatie"></span><br/>
            <strong>Aantal gewenste stagiairs: </strong>
            <span id="aantalGewensteStagiairs"></span><br/>
            <strong>Omgeving: </strong>
            <span id="omgeving"></span><br/>
            <strong>Afstudeerrichting: </strong>
            <span id="afstudeerrichting"></span><br/>
            <strong>Inleidende activiteiten / verwachtingen:</strong>
            <span id="verwachtingen"></span><br/>
        </div>
        <div class="col-md-6">
            <strong>Periode: </strong>
            <span id="periode"></span><br/>
            <strong>Aantal medewerkers: </strong>
            <span id="aantalWerknemers"></span><br/>
            <strong>Aantal IT-medewerkers: </strong>
            <span id="aantalITWerknemers"></span><br/>
            <strong>Aantal IT-medewerkers (begeleiders): </strong>
            <span id="aantalITBegeleiders"></span>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <h3>
                Stageopdracht
            </h3>
            <strong>Omschrijving</strong><br/>
            <p id="omschrijving"></p>
            <strong>Randvoorwaarden</strong><br/>
            <p id="randvoorwaarden"></p>
            <strong>Onderzoeksthema</strong><br/>
            <p id="onderzoeksthema"></p>
            <strong>Beschrijving technische omgeving</strong><br/>
            <p id="beschrijvingTechnischeOmgeving"></p>
            <strong>Bemerkingen</strong><br/>
            <p id="bemerkingen"></p>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <form role="form" action="wijzigenStagevoorstel.php" method="post" id="id-form">
                <input type="hidden" id="id_opdracht" name="id_opdracht" value="#">
                <button type="submit" class="btn btn-indienen" id="wijzigenButton">
                    Stagevoorstel bewerken
                </button>
            </form>
        </div>
    </div>
    <br>
    <br>
</div>

<script src="assets/js/jquery.min.js"></script>
<script src="assets/js/bootstrap.min.js"></script>
<script src="assets/js/scriptHome.js"></script>
<script src="assets/js/initializeDatatables.js"></script>
<script src="assets/js/dataTransfer.js"></script>
<script src="assets/js/sessionTimeout.js"></script>
<script src="assets/js/enumParser.js"></script>
<script src="assets/js/enumParser.js"></script>
<script src="assets/js/ajax_calls.js"></script>
<script src="assets/js/apiConnection.js"></script>

<script>
    $(document).ready(function () {
        var stagevoorstel = getFullDetail(<?php echo $_POST['id_opdracht']; ?>);
        checkForm();

        $("#id-form").submit(function (e) {
            setHiddenField('id_opdracht', stagevoorstel.Id);
            submitForm('id-form');
        });

        function checkForm(){
            if (stagevoorstel.Stageopdracht.Status !== 0 && stagevoorstel.Stageopdracht.Status !== 1) {
                $('#id-form').hide();
            }
        }
    });
</script>
</body>
</html>