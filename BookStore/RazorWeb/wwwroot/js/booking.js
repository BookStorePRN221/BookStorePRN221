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

