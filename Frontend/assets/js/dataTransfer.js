/**
 * Created by 11501537 on 15/04/2017.
 */

function setFormAction($formId, $action){
    document.getElementById($formId).action = $action;
}

function submitForm($formId){
    document.getElementById($formId).submit();
}

function setHiddenField($fieldId, $data){
    document.getElementById($fieldId).value = $data;
}