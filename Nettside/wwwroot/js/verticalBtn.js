const menuButton = document.getElementById('menuButton');
const closeMenuButton = document.getElementById('closeMenuButton');
const verticalMenu = document.getElementById('verticalMenu');
const menuOverlay = document.getElementById('menuOverlay');

// Open the menu when clicking the menubar icon
menuButton.addEventListener('click', (event) => {
    event.preventDefault(); // Prevent the default link behavior
    verticalMenu.classList.add('active');
    menuOverlay.classList.add('active');
});

// Close the menu when clicking the close button
closeMenuButton.addEventListener('click', () => {
    verticalMenu.classList.remove('active');
    menuOverlay.classList.remove('active');
});

// Close the menu when clicking the overlay
menuOverlay.addEventListener('click', () => {
    verticalMenu.classList.remove('active');
    menuOverlay.classList.remove('active');
});
