export class Soba
{
    constructor(ID, Naziv,)
    {
        this.ID = ID;
        this.Naziv = Naziv;
       


    }

    Crtaj(host)
    {
        this.cleanPage(host);
        var div = document.createElement("div");
        host.appendChild(div);
        div.innerHTML = this.Naziv;
        div.className = "Soba";
        var tabela = document.createElement("table");
        host.appendChild(tabela);
        var r = document.createElement("tr");
        tabela.appendChild(r);
        var h1 = document.createElement("th");
        var h2 = document.createElement("th");
        var h3 = document.createElement("th");
        var h4 = document.createElement("th");
        var h5 = document.createElement("th");
        h1.innerHTML = "Ime";
        h2.innerHTML = "Prezime";
        h3.innerHTML = "Email";
        h4.innerHTML = "Vreme pocetka zakazivanja";
        h5.innerHTML = "Vreme kraja zakazivanja";
        
        r.appendChild(h1);
        r.appendChild(h2);
        r.appendChild(h3);
        r.appendChild(h4);
        r.appendChild(h5);
        
        fetch("https://localhost:5001/Soba/GetZakazivanja/"+this.ID).then(
            response => {
            if(response.ok)
            
            {
                var x;  
                response.json().then( Zakazivanja => {
                        
                    Zakazivanja.forEach(Zakazivanje => {
                        
                        var tr = document.createElement("tr");
                        host.appendChild(tr);

                        var dt1 = document.createElement("td");
                        dt1.innerHTML = Zakazivanje.ime;
                        tr.appendChild(dt1);
                        dt1 = document.createElement("td");
                        dt1.innerHTML = Zakazivanje.prezime;
                        tr.appendChild(dt1);
                        dt1 = document.createElement("td");
                        dt1.innerHTML = Zakazivanje.email;
                        tr.appendChild(dt1);

                        dt1 = document.createElement("td");
                        dt1.innerHTML = Zakazivanje.startTime;
                        tr.appendChild(dt1);
                        dt1 = document.createElement("td");
                        dt1.innerHTML = Zakazivanje.endTime;
                        tr.appendChild(dt1);

                        
                      tabela.appendChild(tr);    
                        
                    

                    });
                    
                 


                } );
            }
       });
    }

    cleanPage(host)
    {
        while (host.lastElementChild)
        {
            host.removeChild(host.lastElementChild);
        }
    }
}