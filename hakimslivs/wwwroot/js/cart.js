// Send cart to server and get a list of products back
function LoadCart() {
    var JsonLocalStorageObj = JSON.stringify(localStorage);

    // Vet inte riktigt varför det funkar, men det funkar :D
    // Fick slåss mot en massa error 400, 405, och 415 :)
    $.ajax({
        url: "/GetCartItems",
        type: "POST",
        data: JsonLocalStorageObj,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            createCardWithItems(result);
        },
        error: function (xhr, status, error) {
            console.log('Error : ' + xhr.responseText);
        }
    });
}

// Wait for the page to load, then update the list
window.addEventListener("load", () => {
    LoadCart();
});

// Convert Json to html
function createCardWithItems(jsonData)
{
    var totalPrice = 0;
    var totalAmount = 0;
    var totalStock = 0;
    var totalItems = 0;
    var totalItemsInCart = 0;

    var json = JSON.parse(jsonData);
    var container = document.querySelector("#allItems");
    removeAllChildren(container);
    
    for (var i = 0; i < json.length; i++) {

        var template = document.querySelector("template");
        var holder = template.content.firstElementChild.cloneNode(true);

        var img = holder.querySelector("#imageURL");
        img.src = json[i].Item.ImageURL;
        img.alt = json[i].Item.Product;

        var product = holder.querySelector("#product");
        product.textContent = json[i].Item.Product;
        var description = holder.querySelector("#description");
        description.textContent = json[i].Item.Description;

        var quantity = holder.querySelector("#form1");
        quantity.value = json[i].Amount;
        quantity.max = json[i].Item.Stock;

        var unitPrice = holder.querySelector("#unitPrice");
        unitPrice.textContent = json[i].Item.Price + "kr/st";
        var totalUnitPrice = holder.querySelector("#totalUnitPrice");
        totalUnitPrice.textContent = (json[i].Amount) * (json[i].Item.Price) + "kr";
        container.appendChild(holder);


        var amount = json[i].Amount;
        var price = json[i].Item.Price;
        var stock = json[i].Item.Stock;
        //var id = json[i].Item.ID;

        totalPrice += price * amount;
        totalAmount += amount;
        totalStock += stock;
        totalItems += 1;
        totalItemsInCart += 1;
    }

    setValue("totalPrice", totalPrice + "kr");
    setValue("totalPrice2", totalPrice + "kr");
    setValue("totalAmount", totalAmount);
    setValue("totalStock", totalStock);
    setValue("totalItems", totalItems);
    setValue("totalItemsInCart", totalItemsInCart);
}

// check if element exists first
function setValue(id, value) {
    var element = document.getElementById(id);
    if (element) {
        element.innerHTML = value;
    }
}

function removeAllChildren(parent) {
    while (parent.firstChild) {
        parent.removeChild(parent.firstChild);
    }
}


