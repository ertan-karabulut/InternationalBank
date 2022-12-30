function StartLoading() {
    $(".spinner-container").css('display', 'block');
}

function EndLoading() {
    $(".spinner-container").css('display', 'none');
}

function SaveAsFile(fileName, byteBase64) {
    var link = document.createElement('a');
    link.download = fileName;
    link.href = 'data:@file/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,' + byteBase64;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

function OpenModal(ModalId) {
    $(ModalId).modal('show');
}

function CloseModal(ModalId) {
    $(ModalId).modal('hide');
}