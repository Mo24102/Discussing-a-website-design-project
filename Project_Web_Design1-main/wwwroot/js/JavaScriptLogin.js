document.getElementById('LoginForm').addEventListener('submit', function (event) {
    var email = document.getElementById('Email').value.trim();
    var password = document.getElementById('Password').value.trim();
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    var passwordPattern = /^(?=.*[a-zA-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]).{6,}$/;

    if (!email) {
        alert('Please enter your email.');
        event.preventDefault();
        return;
    }

    if (!password) {
        alert('Please enter your password.');
        event.preventDefault();
        return;
    }

    if (!emailPattern.test(email)) {
        alert('Please enter a valid email address.');
        event.preventDefault();
        return;
    }

    if (!passwordPattern.test(password)) {
        alert('Password must be at least 6 characters long and include letters, numbers, and special characters.');
        event.preventDefault();
        return;
    }
});
