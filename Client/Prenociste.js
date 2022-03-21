import { Soba } from "./Soba.js";

export class Prenociste
{
    constructor(ID, naziv, lokacija)
    {
        this.ID = ID;
        this.naziv = naziv;
        this.lokacija = lokacija;

        this.container = null;
        this.sobe = [];
    }

    Crtaj(host)
    {

         this.container = document.createElement("div");
        host.appendChild(this.container);
        this.container.innerHTML = this.naziv;
        this.container.className = "Prenociste";
        this.container.onclick = (ev) => this.GetSobe();
        
    }

    GetSobe()
    {
        
        fetch("https://localhost:5001/Soba/GetSobe/" + this.ID).then(p => 
        {
            if(p.ok) 
            {
                
                p.json().then(sobejson => sobejson.forEach(element => 
                    {
                    let x = new Soba(element.id, element.naziv, element.cena)
                    this.sobe.push(x);
                    x.Crtaj(this.container);
                }));

            }
        });
        
    }

}