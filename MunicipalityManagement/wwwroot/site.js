document.addEventListener('DOMContentLoaded', function () {
    const successMessage = document.querySelector('#success-message');
    if (successMessage) {
        setTimeout(() => {
            successMessage.style.display = 'none';
        }, 3000); 
    }
});
