document.addEventListener('DOMContentLoaded', () => {
    const registerForm = document.querySelector('form');
    registerForm.addEventListener('submit', (event) => {
        event.preventDefault();
        
        // Obter dados do formulário
        const $nome = document.getElementById('nome').value;
        const $email = document.getElementById('email').value;
        const $senha = document.getElementById('senha').value;

        // Verificar se os campos estão preenchidos
        if ($nome === '' || $email === '' || $senha === '') {
            alert('Por favor preencha todos os campos');
            return;
        }

        // Enviar dados para o backend
        fetch('http://localhost:5000/api/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ $nome, $email, $senha }),
        })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('Cadastro realizado com sucesso!');

                const userDetails = { $email: $email, $senha: $senha };
                localStorage.setItem('user', JSON.stringify(userDetails));

                window.location.href = '/Pages/Login.html';
            } else {
                alert('Falha no registro. Por favor, tente novamente.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('Ocorreu um erro. Por favor, tente novamente.');
        });
    });
});
