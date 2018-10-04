/**
 * Created by Spape on 25/05/2017.
 */
function getFullDetail(id) {
    var returnData = null;
    $.ajax({
        url: BaseURL+"/stagevoorstellen/detail/" + id,
        type: "Get",
        dataType: "json",
        async: false,
        beforeSend: function (xhr) {
            xhr.setRequestHeader('Authorization', 'Bearer ' + sessionStorage.getItem('tokenKey'));
        },
        success: function (data) {
            $('#titel').html(data.Opdrachtgever.Bedrijfsnaam + " : " + data.Stageopdracht.Omgeving);
            $('#time').html("Insgestuurd op: " + data.TimeStamp);

            //Bedrijf
            $('#bedrijfsnaam').html(data.Opdrachtgever.Bedrijfsnaam);
            $('#adres').html(data.Opdrachtgever.Adres + " " + data.Opdrachtgever.Huisnummer);
            $('#gemeente').html(data.Opdrachtgever.Postcode + " " + data.Opdrachtgever.Gemeente);
            $('#telefoonnummer').html(data.Opdrachtgever.Telefoonnummer);

            //Contactpersoon
            $('#naamContactpersoon').html(data.Stageopdracht.Contactpersoon.Achternaam + " " + data.Stageopdracht.Contactpersoon.Voornaam);
            $('#emailContactpersoon').html(data.Stageopdracht.Contactpersoon.Email);
            $('#telefoonnummerContactpersoon').html(data.Stageopdracht.Contactpersoon.Telefoonnummer);

            //Bedrijspromotor
            $('#naamBedrijfspromotor').html(data.Stageopdracht.Bedrijfspromotor.Achternaam + " " + data.Stageopdracht.Bedrijfspromotor.Voornaam);
            $('#emailBedrijfspromotor').html(data.Stageopdracht.Bedrijfspromotor.Email);
            $('#telefoonnummerBedrijfspromotor').html(data.Stageopdracht.Bedrijfspromotor.Telefoonnummer);

            //Technische informatie
            $('#locatie').html(data.Stageopdracht.Locatie);
            $('#aantalGewensteStagiairs').html(data.Stageopdracht.AantalGewensteStagiairs);
            $('#omgeving').html(data.Stageopdracht.Omgeving);
            $('#afstudeerrichting').html(getAfstudeerRichting(data.Stageopdracht.VoorkeurAfstudeerrichting));
            $('#periode').html(getPeriode(data.Stageopdracht.Periode));
            $('#aantalWerknemers').html(data.Stageopdracht.AantalWerknemers);
            $('#aantalITWerknemers').html(data.Stageopdracht.AantalITWerknemers);
            $('#aantalITBegeleiders').html(data.Stageopdracht.AantalITBegeleiders);


            //Stageopdracht
            $('#omschrijving').html(data.Stageopdracht.Omschrijving);
            $('#verwachtingen').html(getInleidendeActiviteit(data.Stageopdracht.InleidendeActiviteiten));
            $('#randvoorwaarden').html(data.Stageopdracht.Randvoorwaarden);
            $('#onderzoeksthema').html(data.Stageopdracht.Onderzoeksthema);
            $('#beschrijvingTechnischeOmgeving').html(data.Stageopdracht.BeschrijvingTechnischeOmgeving);
            $('#bemerkingen').html(data.Bemerkingen);
            returnData = data;
        },
        error: function (data, jqXHR, textStatus, errorThrown) {
            Console.error("Error: Status: " + textStatus + " Message: " + errorThrown + data);
        }
    });
    return returnData;
}