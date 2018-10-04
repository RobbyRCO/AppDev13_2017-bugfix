/**
 * Created by Spapen on 20/05/2017.
 */
function getAfstudeerRichting(afstudeerrichting) {
    if (afstudeerrichting === 0) {
        afstudeerrichting = "Applicatie ontwikkeling";
    } else if (afstudeerrichting === 1) {
        afstudeerrichting = "Systeem en netwerkbeheer";
    } else if (afstudeerrichting === 2) {
        afstudeerrichting = "Softwaremanagement";
    }
    return afstudeerrichting;
}

function getInleidendeActiviteit(inleidendeActiviteit){
    if(inleidendeActiviteit === 0){
        inleidendeActiviteit = "CV";
    }else if(inleidendeActiviteit === 1){
        inleidendeActiviteit = "Solicitatiegesprek";
    }else if(inleidendeActiviteit === 2){
        inleidendeActiviteit = "Vergoeding";
    }
    return inleidendeActiviteit
}

function getPeriode(periode){
    if(periode === 0){
        periode = "Semester 1";
    }else{
        periode = "Semester 2";
    }
    return periode;
}