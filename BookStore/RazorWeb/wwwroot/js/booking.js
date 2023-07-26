function win(userId) {
    console.log(userId);
    var checkboxes = document.getElementsByName('myCheckBox');
    var checkedValues = [];

    for (var i = 0; i < checkboxes.length; i++) {
        if (checkboxes[i].checked) {
            checkedValues.push(checkboxes[i].value);
        }
    }

    requests = [];

    async function getObjectById() {
        const url1 = `https://book0209.azurewebsites.net/api/request/getRequestById?requestId=${id}`;
        // Make a GET request for the object with the specified ID
        const response = await fetch(url1);

        // Check if the request was successful
        if (response.ok) {
            // Parse the response JSON
            const data = await response.json();
            requests.push(data);
        } else {
            throw new Error("Request failed for ID: " + id);
        }
    }

    async function fetchData() {
        for (let i = 0; i < checkedValues.length; i++) {
            id = checkedValues[i];
            await getObjectById(id);
        }

        

        var importationAmount = 0;
        var importationQuantity = 0;

        requests.map((item) => {
            importationAmount += item.request_Amount;
            importationQuantity += item.request_Quantity;
        });

        const url2 = `https://book0209.azurewebsites.net/api/importation/createImportation`;
        console.log(userId);
        fetch(url2, {
            method: "POST",
            headers: { "Content-type": "application/json" },
            body: JSON.stringify({
                import_Id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                user_Id: userId,
                import_Quantity: importationQuantity,
                import_Amount: importationAmount,
                import_Date_Done: "2023-07-14T14:24:37.426Z",
                is_Import_Status: 1,
            }),
        })
            .then(async function () {
                const url3 = `https://book0209.azurewebsites.net/api/importation/getImportIdJustCreated`;
                const getTodo3 = async (url3) => {
                    return await fetch(url3);
                };
                const todo3 = await getTodo3(url3);
                const data3 = await todo3.json();

                const url4 = `https://book0209.azurewebsites.net/api/importationDetail/createImportationDetail`;

                for (let i = 0; i < requests.length; i++) {
                    const el = requests[i];

                    fetch(url4, {
                        method: "POST",
                        headers: { "Content-type": "application/json" },
                        body: JSON.stringify({
                            import_Detail_Id: "3fa85f64-5717-4562-b3fc-2c963f66afa6",
                            request_Id: el.request_Id,
                            import_Id: data3,
                            book_Id: el.book_Id,
                            import_Detail_Quantity: el.request_Quantity,
                            import_Detail_Price: el.request_Price,
                            import_Detail_Amount: el.request_Amount,
                        }),
                    });
                }
                Swal.fire({
                    icon: "success",
                    title: "Add successfully",
                    text: "Items have been move to importation.",
                });
            })
            .catch(function (err) {
                console.log("err: ", err);
            });
    }

    fetchData();

}
async function booking(id) {
    
    const url1 = `https://book0209.azurewebsites.net/api/request/getRequestById?requestId=${id}`
    const getTodo = async (url1) => {
        return await fetch(url1);
    };

    const todo = await getTodo(url1);
    const data = await todo.json();
    console.log("data: ", data);
    document.getElementById("name").value = data.request_Book_Name
    document.getElementById("request").value = data.request_Id
    document.getElementById("requestBook").value = data.request_Id
    document.getElementById("url").value = data.request_Image_Url
    document.getElementById("categoryId").value = data.category_Id
    document.getElementById("quantity").value = data.request_Quantity
    document.getElementById("price").value = data.request_Price
   /* document.getElementById("name").innerHTML = data.request_Book_Name*/
}


