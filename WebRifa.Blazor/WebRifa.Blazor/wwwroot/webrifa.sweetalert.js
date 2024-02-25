function showConfirmationMessage(title, message, acceptButtonMessage, declineButtonMessage) {
    return new Promise((resolve) => {
        Swal.fire({
            title: title,
            html: "<p style='text-align: center'>" + message + "</p>",
            icon: "question",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: acceptButtonMessage,
            cancelButtonText: declineButtonMessage
        }).then((result) => {
            if (result.value) {
                resolve(true);
            } else {
                resolve(false);
            }
        });
    });
}

function showErrorMessage(title, message) {
    return new Promise(() => {
        Swal.fire({
            icon: "error",
            title: title,
            text: message,
        });
    });
}