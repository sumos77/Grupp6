// Send cart to server and get a list of products back
function LoadCart() {

    var JsonLocalStorageObj = JSON.stringify(localStorage);
    //var test = ["test1", "test2", "test3"];

    // Vet inte riktigt varför det funkar, men det funkar :D
    // Fick slåss mot en massa error 400, 405, och 415 :)
    $.ajax({
        url: "/GetCartItems",
        type: "POST",
        data: JsonLocalStorageObj,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            //alert(result);
            //console.log(result);
            createCardWithItems(result);
        },
        error: function (xhr, status, error) {
            console.log('Error : ' + xhr.responseText);
        }
    });

    //const data = localStorage.getItem("shopping-cart");

    //$.ajax({
    //    type: "POST",
    //    url: "Cart",
    //    data: data, //'{name: "' + $("#<%=localStorage.ClientID%>")[0].value + '" }',
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: OnSuccess,
    //    failure: function(response) {
    //        alert(response.d);
    //    }
    //});
}

//function OnSuccess(response) {
//    alert(response.d);
//}



// ---------------------------------------------------------------
// Warning: Ugly code ahead
// ---------------------------------------------------------------
// Wait for the page to load, then update the list
window.addEventListener("load", () => {
    LoadCart();
});

// Convert Json to html
function createCardWithItems(jsonData)
// [{"Item":{"ID":1,"Category":null,"Product":"Coca Cola","Price":11.00,"Stock":55,"Description":"Originalet","ImageURL":"https://images.unsplash.com/photo-1554866585-cd94860890b7?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=465&q=80"},"Amount":1},{"Item":{"ID":2,"Category":null,"Product":"Gurka","Price":15.00,"Stock":50,"Description":"Grönt är gött","ImageURL":"https://images.unsplash.com/photo-1449300079323-02e209d9d3a6?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=774&q=80"},"Amount":1},{"Item":{"ID":14,"Category":null,"Product":"Banan","Price":5.50,"Stock":100,"Description":"En lång gul böjd frukt","ImageURL":"https://images.unsplash.com/photo-1528825871115-3581a5387919?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=415&q=80g"},"Amount":2}]
{
    var html = "";
    var totalPrice = 0;
    var totalAmount = 0;
    var totalStock = 0;
    var totalItems = 0;
    var totalItemsInCart = 0;
    var html = '';
    var currency = "kr";
    
    // convert string to json object
    var json = JSON.parse(jsonData);
    //console.log(jsonData);
    //console.log("--------------");
    //console.log(json);
    //console.log("--------------");
    
    for (var i = 0; i < json.length; i++) {
        var amount = json[i].Amount;
        var price = json[i].Item.Price;
        var stock = json[i].Item.Stock;
        var product = json[i].Item.Product;
        var description = json[i].Item.Description;
        var imageURL = json[i].Item.ImageURL;
        var id = json[i].Item.ID;

        totalPrice += price * amount;
        totalAmount += amount;
        totalStock += stock;
        totalItems += 1;
        totalItemsInCart += 1;

        html += `
        <div class="card-body">
            <!-- Single item -->
            <div class="row">
                <div class="col-lg-3 col-md-12 mb-4 mb-lg-0">
                    <!-- Image -->
                    <div class="bg-image hover-overlay hover-zoom ripple rounded" data-mdb-ripple-color="light">
                        <img src="${imageURL}"
                            class="w-100" alt="${product}" />
                        <a href="#!">
                            <div class="mask" style="background-color: rgba(251, 251, 251, 0.2)"></div>
                        </a>    
                    </div>
                    <!-- Image -->
                </div>
                
                <div class="col-lg-5 col-md-6 mb-4 mb-lg-0">
                    <!-- Data -->
                    <p><strong>${product}</strong></p>
                    <p>${description}</p>

                    <button type="button" class="btn btn-danger btn-sm me-1 mb-2" data-mdb-toggle="tooltip" title="Remove item">
                        <i class="fas fa-trash"></i>
                    </button>
                    <!-- Data -->
                </div>

                <div class="col-lg-4 col-md-6 mb-4 mb-lg-0">
                <!-- Quantity -->
                    <div class="d-flex mb-4" style="max-width: 300px">
                        <button class="btn btn-primary px-3 me-2" onclick="this.parentNode.querySelector('input[type=number]').stepDown()">
                            <i class="fas fa-minus"></i>
                        </button>

                        <div class="form-outline">
                            <input id="form1" min="0" name="quantity" value="${amount}" type="number" class="form-control" />
                        </div>
                        
                        <button class="btn btn-primary px-3 ms-2" onclick="this.parentNode.querySelector('input[type=number]').stepUp()">
                            <i class="fas fa-plus"></i>
                        </button>
                    </div>  
                    <!-- Quantity -->
                    <!-- Price -->
                    <p class="text-start text-md-center">
                        <strong>${price * amount} ${currency}</strong>
                    </p>
                    <!-- Price -->
                </div>
            </div>
            <!-- Single item -->

            <hr class="my-4" />
            </div>
        `;
    }

    setValue("totalPrice", totalPrice);
    setValue("totalAmount", totalAmount);
    setValue("totalStock", totalStock);
    setValue("totalItems", totalItems);
    setValue("totalItemsInCart", totalItemsInCart);
    setValue("allItems", html);
    setValue("currency", currency);
}

// check if element exists first
function setValue(id, value) {

    var element = document.getElementById(id);
    if (element) {
        element.innerHTML = value;
    }
}




