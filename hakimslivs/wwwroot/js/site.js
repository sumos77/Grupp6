let shoppingCart = readLocalStorage() || new Map();
let shoppingCartElt = document.getElementsByClassName('bi-cart')[0];

readLocalStorage();
allAddButtons = document.getElementsByClassName('add-to-cart');

function registerHandlers() {
    for (const addButton of allAddButtons) {
        addButton.onclick = event => {
            let productClicked = addButton.name;
            if (shoppingCart.has(productClicked)) {
                let currentQuantity = shoppingCart.get(productClicked);
                shoppingCart.set(productClicked, currentQuantity + 1);
            }
            else {
                shoppingCart.set(productClicked, 1);
            }

            writeLocalStorage();
            numberOfItemsInCart();
            //updateLSElement();
        }
    }
};

function writeLocalStorage() {
    localStorage.setItem('shopping-cart', JSON.stringify(Object.fromEntries(shoppingCart)));
}

function readLocalStorage() {
    let cartStorage = JSON.parse(localStorage.getItem('shopping-cart'));

    if (cartStorage != null) {
        return new Map(Object.entries(cartStorage));
    };
}
function numberOfItemsInCart() {
    let total = 0;

    var mapIter = shoppingCart.values();
    shoppingCart.forEach(value => {
        total += mapIter.next().value;
    });
    let number = document.getElementById("amount");
    if (total !== 0) {
        number.textContent = total + " ";
        amount.style.color = "green";
    }

    if (shoppingCart.size > 0) {
        shoppingCartElt.style.color = "green";
    }
}
//function updateLSElement() {
//    const data = localStorage.getItem("shopping-cart");
//    const input = document.getElementById("localStorage");
//    input.innerHTML = data;
//}

//<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>

registerHandlers();
numberOfItemsInCart();
// updateLSElement();