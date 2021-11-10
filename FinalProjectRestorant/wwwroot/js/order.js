let minus = document.querySelector(".fa-minus-circle");
let plus = document.querySelector(".fa-plus-circle");
let count = document.querySelector(".count");
let foodprc = document.querySelector(".food-price-count");
let allprc = document.querySelector(".all-price-count");

minus.addEventListener('click',()=>{
    
    let priceValue=count.innerText;
     priceValue--; 
     count.innerText=priceValue;
     if(count.innerText < 0){
        alert("min 1 eded sifaris ede bilersiZ")
     }

});
plus.addEventListener('click',()=>{
    let priceValue=count.innerText;
    priceValue++; 
    count.innerText=priceValue;
    allprc.innerText = count.innerText * foodprc.innerText;
})  