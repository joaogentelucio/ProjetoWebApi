const menuIcon = document.getElementById('menu-icon');
const aside = document.querySelector('aside');

menuIcon.addEventListener('click', () => {
    aside.classList.toggle('expanded');
    if (aside.classList.contains('expanded')) {
        aside.style.width = '20%';
        menuIcon.classList.replace('fa-bars', 'fa-xmark');
    } else {
        aside.style.width = '10%';
        menuIcon.classList.replace('fa-xmark', 'fa-bars');
    }
});
