// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//Sidebar TOGGLE 
var sidebarOpen = false;
var sidebar = document.getElementById('sidebar');

function opneSidebar()
{
    if(!sidebarOpen)
    {
        sidebar.classList.add('sidebar_responsive');)
        sidebarOpen = true;
    })
}


function closeSidebar()
{ 
    if(sidebarOpen)
    {
        sidebar.classList.remove('sidebar_responsive');
        sidebarOpen = false;
    })

}