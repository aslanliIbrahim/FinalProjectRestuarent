let minus = document.querySelector(".fa-minus-circle");
let plus = document.querySelector(".fa-plus-circle");
let count = document.querySelector(".count");
let foodprc = document.querySelector(".food-price-count");
let allprc = document.querySelector(".all-price-count");

minus.addEventListener('click',()=>{
    
    let priceValue = count.innerText;
    let intValue = parseInt(priceValue);
    if (intValue != 1) {

        priceValue--;

    }

    count.innerText = priceValue;
    allprc.value = count.innerText * foodprc.value;
});
plus.addEventListener('click',()=>{
    let priceValue=count.innerText;
    priceValue++; 
    count.innerText=priceValue;
    allprc.value = count.innerText * foodprc.value;
})  