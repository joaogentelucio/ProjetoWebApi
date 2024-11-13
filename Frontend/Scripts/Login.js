document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.querySelector('form');
    loginForm.addEventListener('submit', (event) => {
        event.preventDefault();
        
        // Obter dados do formulário
        const $email = document.getElementById('email').value;
        const $senha = document.getElementById('senha').value;

        // Verificar se os campos estão preenchidos
        if ($email === '' || $senha === '') {
            alert('Por favor preencha ambos os campos');
            return;
        }

        // Enviar dados para o backend
        fetch('http://localhost:5000/api/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ $email, $senha }),
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                
                const userDetails = { $email: $email, $senha: $senha };
                localStorage.setItem('user', JSON.stringify(userDetails));
          
                window.location.href = 'Dashboard.html';
            } else {
                alert('Credenciais inválidas');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Ocorreu um erro. Por favor, tente novamente.');
        });
    });
});
