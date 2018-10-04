<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Indienen stagevoorstel</title>

    <!-- Bootstrap & CSS -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="//netdna.bootstrapcdn.com/font-awesome/4.2.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="assets/css/awesome-bootstrap-checkbox.css">
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
            <a href="homescreenBedrijf.php" class="hidden-sm hidden-xs logo"><img src="assets/img/pxllogowitterand.png"></a>
        </div>

        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav">
                <li><a href="homescreenBedrijf.php">Home</a></li>
                <li class="current"><a href="indienenstagevoorstelBedrijf.php">Stagevoorstel indienen</a></li>
                <li><a href="GegevensBijwerkenBedrijf.php">Persoonlijke gegevens</a></li>
            </ul>
        </div>
    </div>
</nav>
<div class="container">
    <h2>
        Voorstel stageopdracht
    </h2>
    <div>
            <span style="text-align: center">
                <p>Via onderstaand formulier kunnen stageopdrachten voor PXL-IT worden ingegeven. Er staat geen limiet op het aantal opdrachten dat ingestuurd kan worden per bedrijf, maar we beperken het aantal stagiairs per bedrijf tot 2. </p>
                <p>Hopend op uw begrip.</p>
            </span>
    </div>
    <span style="color: red">Vereist *</span>
    <form id="stagevoorstelForm">
        <div id="stageopdracht">
            <h3>Stageopdracht</h3>
            <br>
            <label>Voorkeur afstudeerrichting *</label>
            <p class="form-text text-muted">Selecteer hier de afstudeerrichting waarvoor de stageopdracht bestemd is</p>
            <div class="radio radio-info">
                <input type="radio" class="checkbox" name="VoorkeurAfstudeerrichting" id="radio1"
                       value=0>
                <label for="radio1">Applicatie-ontwikkeling</label>
            </div>
            <div class="radio radio-info">
                <input type="radio" name="VoorkeurAfstudeerrichting" id="radio2" value=1 class="btn btn-default">
                <label for="radio2">Systemen en Netwerkbeheer</label>
            </div>
            <div class="radio radio-info">
                <input type="radio" name="VoorkeurAfstudeerrichting" id="radio3" value=2 class="btn btn-default">
                <label for="radio3">Software Management</label>
            </div>
            <br>
            <div class="form-group">
                <label for="omschrijving">Omschrijving van de opdracht *</label>
                <p class="form-text text-muted">Beschrijf hier duidelijk en zo gedetailleerd mogelijk wat de
                    stageopdracht inhoudelijk zal omvatten.</p>
                <textarea name="Omschrijving" id="omschrijving" class="form-control" rows="7" cols="60"
                          required></textarea>
            </div>
            <label>Omgeving *</label>
            <p class="form-text text-muted">In welke IT-omgeving dient de stageopdracht uitgewerkt te worden:
                bijvoorbeeld</p>
            <div class="radio radio-info">
                <input type="radio" name="Omgeving" id="radio4" value="Programmeren: Java">
                <label for="radio4">Programmeren: Java</label>
            </div>
            <div class="radio radio-info">
                <input type="radio" name="Omgeving" id="radio5" value="Programmeren: .Net">
                <label for="radio5">Programmeren: .Net</label>
            </div>
            <div class="radio radio-info">
                <input type="radio" name="Omgeving" id="radio6" value="Web: CSS, Javascript, PHP, Angular, ...">
                <label for="radio6">Web: CSS, Javascript, PHP, Angular, ...</label>
            </div>
            <div class="radio radio-info">
                <input type="radio" name="Omgeving" id="radio7" value="Mobile: Android, iOS, Windows, ...">
                <label for="radio7">Mobile: Android, iOS, Windows, ...</label>
            </div>
            <div class="radio radio-info">
                <input type="radio" name="Omgeving" id="radio8" value="Systemen&Netwerken:  Linux, Windows, ...">
                <label for="radio8">Systemen&Netwerken: Linux, Windows, ...</label>
            </div>
            <div class="radio radio-info">
                <input type="radio" name="Omgeving" id="radio9"
                       value="Software testing, ITIL, Projectmanagement, CRM, Information Management">
                <label for="radio9">Software testing, ITIL, Projectmanagement, CRM, Information Management</label>
            </div>
            <div class="radio radio-info">
                <input type="radio" name="Omgeving" id="radio10">
                <label for="radio10">Anders:</label>
                <input type="text" id="other" class="form-control">
            </div>
            <br>
            <div class="form-group">
                <p class="form-text text-muted">Verdere beschrijving van de technische omgeving</p>
                <input type="text" name="BeschrijvingTechnischeOmgeving" id="beschrijvingTechnischeOmgeving"
                       class="form-control" value="" required>
            </div>
            <div class="form-group">
                <label for="randvoorwaarden">Randvoorwaarden</label>
                <p class="form-text text-muted">Zijn er specifieke eisen waaraan moet worden voldaan voor het uitvoeren
                    van de stageopdracht vb bereidheid tot communicatie in het Engels, beschikken over een auto,
                    bereikbaar met openbaar vervoer,...</p>
                <textarea name="Randvoorwaarden" id="randvoorwaarden" class="form-control" rows="7" cols="60"></textarea>
            </div>
            <div class="form-group">
                <label for="onderzoeksthema">Onderzoeksthema</label>
                <p class="form-text text-muted">Onderzoek dat leidt tot het vergaren van diepgaande kennis voor het
                    oplossen van een praktijkprobleem. Het thema kan zich situeren in het kader of in het verlengde van
                    de stageopdracht ofwel een specifieke onderzoeksopdracht aangebracht door het bedrijf (zie
                    begeleidende brief voor enkele voorbeelden)</p>
                <textarea name="Onderzoeksthema" id="onderzoeksthema" class="form-control" rows="7" cols="60"></textarea>
            </div>
            <br>
            <h3>Overige</h3>
            <div class="row">
                <div class="col-md-4">
                    <label>Inleidende activiteiten / verwachtingen</label>
                    <div class="checkbox checkbox-info">
                        <input type="checkbox" name="InleidendeActiviteiten" id="checkbox1" value=0>
                        <label for="checkbox1">CV</label>
                    </div>
                    <div class="checkbox checkbox-info">
                        <input type="checkbox" name="InleidendeActiviteiten" id="checkbox2" value=1>
                        <label for="checkbox2">Sollicitatiegesprek</label>
                    </div>
                    <div class="checkbox checkbox-info">
                        <input type="checkbox" name="InleidendeActiviteiten" id="checkbox3" value=2>
                        <label for="checkbox3">Vergoeding / tegemoetkoming in verplaatsingskosten</label>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>Aantal gewenste stagiairs *</label>
                    <div class="radio radio-info">
                        <input type="radio" name="AantalGewensteStagiairs" id="radio11" value=1>
                        <label for="radio11">1 student</label>
                    </div>
                    <div class="radio radio-info">
                        <input type="radio" name="AantalGewensteStagiairs" id="radio12" value=2>
                        <label for="radio12">2 studenten</label>
                    </div>
                </div>
                <div class="col-md-4">
                    <label>Stageperiode *</label><br>
                    <div class="radio radio-info">
                        <input type="radio" name="Stageperiode" id="radio13" value=0>
                        <label for="radio13">Semester 1 (19/09/2016-16/12/2016 OF 03/10/2016-13/01/2017)</label>
                    </div>
                    <div class="radio radio-info">
                        <input type="radio" name="Stageperiode" id="radio14" value=1>
                        <label for="radio14">Semester 2 (20/02/2017-02/06/2017)</label>
                    </div>
                </div>
            </div>
            <br>
            <h3>Bedrijf / Opdrachtgever</h3>
            <div class="form-group">
                <label for="locatie">Locatie van de stage (indien elders dan het adres van het
                    stagebedrijf)</label>
                <p class="form-text text-muted">Straat - Nr - Pc - Gemeente</p>
                <textarea name="Locatie" id="locatie" class="form-control" rows="7"
                          cols="60"></textarea>
            </div>
            <br>
            <div class="form-group">
                <label for="aantalWerknemers">Aantal Medewerkers (op het bedrijf) *</label>
                <input type="number" name="AantalWerknemers" id="aantalWerknemers" class="form-control"
                       value="" required>
            </div>
            <div class="form-group">
                <label for="aantalITWerknemers">Aantal IT-medewerkers (op het bedrijf) *</label>
                <input type="number" name="AantalITWerknemers" id="aantalITWerknemers" class="form-control"
                       value="" required>
            </div>
            <div class="form-group">
                <label for="aantalITBegeleiders">Aantal IT medewerkers (op het bedrijf) *</label>
                <p class="form-text text-muted">Die de student technisch kunnen begeleiden bij het uitwerken van zijn
                    stageopdracht</p>
                <input type="number" name="AantalITBegeleiders" id="aantalITBegeleiders" class="form-control"
                       value="" required>
            </div>
        </div>
        <br>
        <div id="contactpersoon">
            <h3>
                Contactpersoon
            </h3>
            <p class="form-text text-muted">Deze persoon zal de stagedocumenten ondertekenen</p>
            <h4>Voeg een nieuwe contactpersoon toe</h4>
            <div class="form-group">
                <label for="title">Titel</label>
                <input type="text" name="Title" id="title" class="form-control" value="">
            </div>
            <div class="form-group">
                <label for="achternaam">Naam</label>
                <input type="text" name="Achternaam" id="achternaam" class="form-control" value="">
            </div>
            <div class="form-group">
                <label for="voornaam">Voornaam</label>
                <input type="text" name="Voornaam" id="voornaam" class="form-control" value="">
            </div>
            <div class="form-group">
                <label for="telefoonnummer">Telefoonnummer</label>
                <p class="form-text text-muted">Formaat: +32 11 123456 of +32 496 123456</p>
                <input type="text" name="Telefoonnummer" id="telefoonnummer" class="form-control"
                       pattern=".*\+?\(?\d{2,4}\)?[\d\s-]{3,}.*" value="">
            </div>
            <div class="form-group">
                <label for="email">E-mail adres contactpersoon</label>
                <input type="text" name="Email" id="email" class="form-control"
                       pattern=".*[a-zA-Z0-9_\.\+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+.*"
                       value="">
            </div>
            <h4>Of kies een bestaande contactpersoon</h4>
            <select class="form-control" id="contactpersonen">
                <option>Geen geselecteerd</option>
            </select>
        </div>
        <br>
        <div id="bedrijfspromotor">
            <h3>Bedrijfspromotor (in het bedrijf)</h3>
            <p class="form-text text-muted">Deze persoon zal de student technisch begeleiden tijdens de stageperiode en
                zal tevens aanwezig
                zijn tijdens het juryexamen van de student</p>
            <h4>Voeg een nieuwe bedrijfspromotor toe</h4>
            <div class="form-group">
                <label for="title">Titel</label>
                <input type="text" name="Title" id="title" class="form-control" value="">
            </div>
            <div class="form-group">
                <label for="achternaam">Naam</label>
                <input type="text" name="Achternaam" id="achternaam" class="form-control" value="">
            </div>
            <div class="form-group">
                <label for="voornaam">Voornaam</label><br>
                <input type="text" name="Voornaam" id="voornaam" class="form-control" value="">
            </div>
            <div class="form-group">
                <label for="telefoonnummer">Telefoonnummer</label>
                <p class="form-text text-muted">Formaat: +32 11 123456 of +32 496 123456</p>
                <input type="text" name="Telefoonnummer" id="telefoonnummer" class="form-control"
                       pattern=".*\+?\(?\d{2,4}\)?[\d\s-]{3,}.*" value="">
            </div>
            <div class="form-group">
                <label for="email">E-mail adres bedrijfspromtor</label>
                <input type="text" name="Email" id="email" class="form-control"
                       pattern=".*[a-zA-Z0-9_\.\+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-\.]+.*"
                       value="">
            </div>
            <h4>Of kies een bestaande bedrijfspromtor</h4>
            <select class="form-control" id="bedrijfspromotors">
                <option>Geen geselecteerd</option>
            </select>
            <br>
            <br>
            <div class="form-group">
                <label for="bemerking">Andere bemerkingen</label>
                <input type="text" id="bemerking" class="form-control" value="" required>
            </div>
        </div>
        <br>
        <button type="submit" value="Verzenden" class="verzenden" id="sendButton">Verzenden</button>
        <br>
        <br>
    </form>
</div>

<script src="assets/js/jquery.min.js"></script>
<script src="assets/js/bootstrap.min.js"></script>

<script src="assets/js/sessionTimeout.js"></script>
<script src="assets/js/apiConnection.js"></script>
<script>
    $(document).ready(function () {
        var contactpersonen = document.getElementById("contactpersonen");
        var bedrijfspromotors = document.getElementById("bedrijfspromotors");
        $.ajax({
            url: BaseURL+"/bedrijf/contactpersoonAndBedrijfspromotors/" + sessionStorage.getItem("userId"),
            type: "Get",
            dataType: "json",
            async: false,
            beforeSend: function (xhr) {
                xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
            },
            success: function (data) {
                for (var i = 0; i < data.Contactpersonen.length; i++) {
                    contactpersonen[i + 1] = new Option(data.Contactpersonen[i].Voornaam + " " + data.Contactpersonen[i].Achternaam, JSON.stringify(data.Contactpersonen[i].Id));
                }

                for (i = 0; i < data.Bedrijfspromotors.length; i++) {
                    bedrijfspromotors[i + 1] = new Option(data.Bedrijfspromotors[i].Voornaam + " " + data.Bedrijfspromotors[i].Achternaam, JSON.stringify(data.Bedrijfspromotors[i].Id));
                }
            },
            error: function (data, jqXHR, textStatus, errorThrown) {
                alert("Error: Status: " + textStatus + " Message: " + errorThrown + data);
            }
        });

        $('#stagevoorstelForm').submit(function (e) {
            e.preventDefault();
            var stageopdracht = objectifyForm($('#stageopdracht :input').serializeArray());
            stageopdracht.Status = 0;
            stageopdracht.OpdrachtgeverId = sessionStorage.getItem("userId");
            if (contactpersonen.selectedIndex === 0) {
                stageopdracht.Contactpersoon = objectifyForm($('#contactpersoon :input').serializeArray());
                stageopdracht.Contactpersoon.BedrijfInDienstId = sessionStorage.getItem("userId");
            } else {
                stageopdracht.ContactpersoonId = $('#contactpersonen').val();
            }
            if (bedrijfspromotors.selectedIndex === 0) {
                stageopdracht.Bedrijfspromotor = objectifyForm($('#bedrijfspromotor :input').serializeArray());
                stageopdracht.Bedrijfspromotor.BedrijfInDienstId = sessionStorage.getItem("userId");
            } else {
                stageopdracht.BedrijfspromotorId = $('#bedrijfspromotors').val();
            }
            if ($('#radio10').prop('checked')) {
                stageopdracht.Omgeving = document.getElementById("other").value;
            }
            var stagevoorstel = {
                "Timestamp": new Date().toISOString().slice(0, 19).replace('T', ' '),
                "Verstuurd": true,
                "Stageopdracht": stageopdracht,
                "OpdrachtgeverId": sessionStorage.getItem("userId"),
                "Bemerkingen": document.getElementById("bemerking").value,
                "StagecoördinatorBehandelingLectorId": 1
            };
            postData(stagevoorstel);
        });

        function objectifyForm(formArray) {

            var returnArray = {};
            for (var i = 0; i < formArray.length; i++) {
                returnArray[formArray[i]['name']] = formArray[i]['value'];
            }
            return returnArray;
        }

        function postData(data) {
            $.ajax({
                url: BaseURL+'/stagevoorstellen',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify(data),
                beforeSend: function (xhr) {
                    xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
                },
                success: function () {
                    window.location.href = "homescreenBedrijf.php";
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("Error: Status: " + textStatus + " Message: " + errorThrown);
                }
            });
        }
    });
</script>
</body>
</html>