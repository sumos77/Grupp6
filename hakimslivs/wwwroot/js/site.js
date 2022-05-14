let shoppingCart = readLocalStorage() || new Map();
let shoppingCartElt = document.getElementsByClassName('bi-cart')[0];

readLocalStorage();
allAddButtons = document.getElementsByClassName('add-to-cart');

//Search by category and product whenever a button is pressed and redraws the page.
function searchOnClick(){
    let searchIndex = [];
    //Builds two arrays from what is currently displayed on the page
    currentPageProducts = Array.from(document.getElementsByClassName('product-search-name'));
    currentPageCategories = Array.from(document.getElementsByClassName('category-link'));
    //Turns all the values in those two arrays into lowercase and add them to the searchIndex list
    currentPageProducts.forEach(item => searchIndex.push(item.textContent.toLowerCase()));
    currentPageCategories.forEach(item => searchIndex.push(item.textContent.toLowerCase()));
    //The result is an array-like object created by the funtion getElementsByClassName, there's only one search bar so we only need the first one
    inputElt = document.getElementsByClassName('search-bar-content')[0];
    
    //Whenever a key is released this eventhandler fires
    inputElt.addEventListener('keyup', (event) =>{
        searchTerm = inputElt.value.toLowerCase();
        const result = searchIndex.filter(word => word.includes(searchTerm));
        //All cards that are currently displayed
        currentPageCards = Array.from(document.getElementsByClassName('card'));
        //Filters which cards to display
        currentPageCards.forEach(item => {
            cardTitle = item.getElementsByClassName('product-search-name')[0].textContent.toLowerCase();
            cardCategory = item.getElementsByClassName('category-link')[0].textContent.toLowerCase();
            if (result.includes(cardTitle) || result.includes(cardCategory)){
                item.style.display='flex';
            }
            else {
                item.style.display = 'none';
            }
            
        })
    });
}


function registerHandlers() {
    for (const addButton of allAddButtons) {
        addButton.onclick = event => {
            let productClicked = addButton.name;
            var stock = (addButton.title);
            if (shoppingCart.has(productClicked)) {
                let currentQuantity = shoppingCart.get(productClicked);
                if (!(currentQuantity >= stock)) {
                    shoppingCart.set(productClicked, currentQuantity + 1);
                }
                else {
                    addButton.textContent = "Slut"
                    addButton.disabled = true;
                }
            }
            else {
                shoppingCart.set(productClicked, 1);
            }

            writeLocalStorage();
            numberOfItemsInCart();
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

registerHandlers();
numberOfItemsInCart();
searchOnClick();
