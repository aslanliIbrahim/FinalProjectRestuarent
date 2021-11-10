let nav = document.querySelector("nav")
let header = document.querySelector("header")


//click hamburger menu
let selectElement = (element) => document.querySelector(element);
let navActive = document.querySelector('nav')
selectElement = ('.menu-icons'), addEventListener('click', ()=>{
    navActive.classList.toggle('active');
})
//end hamburger menu

document.onscroll=function(e){
    if(document.documentElement.scrollTop > 100)
    {
        document.querySelector('header').classList.add("stick");
    }else{
        document.querySelector('header').classList.remove("stick");
    }
    // else 
    // {
    //     (".navbar").removeClass(".stick");
    // }
}