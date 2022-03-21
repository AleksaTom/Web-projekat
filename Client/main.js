import { Korisnik } from "./Korisnik.js";
import { Soba } from "./Soba.js";
import { Prenociste } from "./Prenociste.js";
import {Zakazivanje} from "./Zakazivanje.js";

class Main
{
    constructor()
    {
        this.username = "";
        this.password = "";
        this.Korisnik = null;
        this.MainContainer = null;
    }

    Crtaj()
    {
        
        this.MainContainer = document.createElement("div");
        this.MainContainer.className = "MainContainer";
        var navigation = document.createElement("div");
        navigation.className = "navigation";
        this.MainContainer.appendChild(navigation);
        var butlog = document.createElement("div");
        navigation.appendChild(butlog);
        var body = document.createElement("div");
        body.className = "MainBody";
        this.MainContainer.appendChild(body);
        butlog.onclick = (ev) => this.crtajLogin(butlog, body, navigation);
        butlog.innerHTML = "Prijavi se";
        butlog.className = "navbtn";
        document.body.appendChild(this.MainContainer);

        var register = document.createElement("div");
        register.innerHTML="Registracija";
        register.className="navbtn";
        navigation.appendChild(register);
        register.onclick = (ev) => this.crtajRegisterUser(body);

        var div1 = document.createElement("div");
        div1.className = "dobrodosli";
        var h1 = document.createElement("h1");
        h1.innerHTML = "Dobrodošli";
        var p = document.createElement("p");
        p.innerHTML = "Registrujte se i rezervišite svoj termin za odmor što pre";
        var p1 = document.createElement("p");
        p1.innerHTML = "Želimo Vam lep provod!";
        div1.appendChild(h1);
        div1.appendChild(p);
        div1.appendChild(p1);
        body.appendChild(div1);

    }
    crtajLogin(btnLog, host, navigation)
    {
        this.cleanPage(host);

        var div = document.createElement("div");
        div.className = "postForm";
        div.className += " Form";
        host.appendChild(div);

        var label = document.createElement("label");
        label.innerHTML = "Korisničko ime:"
        label.htmlFor = "usernameForm";
        div.appendChild(label);
        var username = document.createElement("input");
        username.type = "text";
        username.placeholder = "username";
        div.appendChild(username);
        
        
        label = document.createElement("label");
        label.htmlFor = "passwordForm";
        label.innerHTML = "Lozinka:"
        div.appendChild(label);
        var sifra = document.createElement("input");
        sifra.type = "password";
        div.appendChild(sifra);  

        var el = document.createElement("button");
        el.innerHTML="Prijavi se";
        el.onclick = (ev) => this.login(username.value, sifra.value, btnLog, navigation, host); 
        div.appendChild(el);

    }


    login(username, sifra, btnLog, navigation, host)
    {
        if(username===null || username===undefined || username==="")
        {
            alert("Unesite korisničko ime");
            return;
        }
        if(sifra===null || sifra===undefined || sifra==="")
        {
            alert("Unesite lozinku");
            return;
        }
        fetch("https://localhost:5001/Korisnik/LogIn/",
        {
            method:"POST",
            headers:{
                "Content-Type":"application/json"
            },
            body:JSON.stringify(
                {   
                    "username" : username,
                    "password" : sifra

                }
            )

        }).then(response => {
            if(response.ok)
            {
                response.json().then(usr => {
                    this.username = usr.username;
                    this.password = usr.sifra;
                    this.Korisnik = new Korisnik(usr.id, usr.username, usr.password, usr.ime, usr.prezime, usr.email);
                 

                });
                alert("Uspešno ste se prijavili na sajt!");
                this.cleanPage(host);    
                btnLog.innerHTML = "Logout";

                var d1 = document.createElement("div");
                d1.className = "dobrodosli";
                var h1 = document.createElement("h1");
                h1.innerText = "Hvala na prijavi!";
                var p1 = document.createElement("p");
                p1.innerHTML = "Rezervišite svoj smeštaj klikom na 'Zakaži' ili";
                var p2 = document.createElement("p");
                p2.innerHTML = "možete pogledati svoje rezervacije klikom na 'Vaša zakazivanja' sa leve strane";

                d1.appendChild(h1);
                d1.appendChild(p1);
                d1.appendChild(p2);
                host.appendChild(d1);
              
                var div1 = document.createElement("div");
                div1.innerHTML = "Zakaži";
                navigation.appendChild(div1);
                div1.className = "navbtn";
                
                var div2 = document.createElement("div");
                
                div2.innerHTML = "Vasa zakazivanja";
                div2.className = "navbtn";
                navigation.appendChild(div2);
                div1.onclick = (ev) => this.crtajZakazivanja(host); 
                div2.onclick = (ev) => this.CrtajUserZakazivanje(host);   
                btnLog.onclick = (ev) => this.logout(btnLog, host, div1, div2);
            }
            else
            {
                alert("Doslo je do greske");
            }    
        });
    }

    crtajRegisterUser(host)
    {
    
        this.cleanPage(host);


        var div = document.createElement("div");
        div.className = "registerUserForm";
        div.className += " Form";
        host.appendChild(div);
        

        var label = document.createElement("label");
        label.innerHTML = "Username:"
        div.appendChild(label);
        var username = document.createElement("input");
        username.type = "text";
        username.value = "username";
        div.appendChild(username);

        label = document.createElement("label");
        label.innerHTML = "Password:";
        div.appendChild(label);
        var sifra = document.createElement("input");
        sifra.type = "password";
        sifra.value ="sifra";
        div.appendChild(sifra);

        label = document.createElement("label");
        label.innerHTML = "Mail:";
        div.appendChild(label);
        var mail = document.createElement("input");
        mail.type = "text";
        mail.value = "mail";
        div.appendChild(mail);

        label = document.createElement("label");
        label.innerHTML = "Name:";
        div.appendChild(label);
        var ime = document.createElement("input");
        ime.type = "text";
        ime.value ="ime";
        div.appendChild(ime);

        label = document.createElement("label");
        label.innerHTML = "Surname:";
        div.appendChild(label);
        var prezime = document.createElement("input");
        prezime.type = "text";
        prezime.value = "prezime";
        div.appendChild(prezime);



        var btn = document.createElement("button");
        btn.innerHTML="Register";
        btn.onclick = (ev) => this.fetchRegister(username.value, sifra.value, ime.value, prezime.value, mail.value);
        div.appendChild(btn);

    }

    fetchRegister(username, sifra, ime, prezime, mail)
    {
    
        if(username===null || username===undefined || username==="")
        {
            alert("unesite username");
            return;
        }
        if(sifra===null || sifra===undefined || sifra==="")
        {
            alert("unesite password");
            return;
        }
        if(ime===null || ime===undefined || ime==="")
        {
            alert("unesite ime");
            return;
        }
        if(prezime===null || prezime===undefined || prezime==="")
        {
            alert("unesite prezime");
            return;
        }
        if(mail===null || mail===undefined || mail==="")
        {
            alert("unesite mail");
            return;
        }
        if(ime.length > 50)
        {
            alert("Predugacko ime");
            return;
        }
        if(prezime.length > 50)
        {
            alert("Predugacko prezime");
            return;
        }
        if(sifra.length > 50)
        {
            alert("Predugacka sifra");
            return;
        }
        if(mail.length > 50)
        {
            alert("Predugacki email");
            return;
        }
        if(username.length > 50)
        {
            alert("Predugacki username");
            return;
        }

        fetch("https://localhost:5001/Korisnik/Register", {
            method:"POST",
            headers: 
            {
                "Content-Type":"application/json"
            },
            body:
            JSON.stringify(
                {
                    "username":username,
                    "email":mail,
                    "password":sifra,
                    "ime":ime,
                    "prezime":prezime
                }
            )
        })
        .then(p => {
           
            if(p.ok)
                alert("Korisnik je uspesno dodat");
            else
                alert("Doslo je do greske");
            
        });


    }
   

    CrtajUserZakazivanje(host)
    {
        this.cleanPage(host);

        fetch("https://localhost:5001/Zakazivanje/GET_SINGLE_ZAKAZIVANJA/" + this.Korisnik.id).then(r => {
            
                if(r.ok)
                {
                    r.json().then(zakazivanja => {
                      
                        zakazivanja.forEach(zakaz => {
                     
                            let x = new Zakazivanje(zakaz.id, this.Korisnik.id, zakaz.soba, zakaz.prenociste, zakaz.starttime, zakaz.endtime)
                            x.CrtajZaKorisnika(host);
                        })
                    })
                }
        })

    }
    crtajZakazivanja(host)
    {
        this.cleanPage(host);
        var ZakaziForma = document.createElement("div");
        host.appendChild(ZakaziForma);
        var SpcSelect = document.createElement("select");
        ZakaziForma.appendChild(SpcSelect);
        var SobaSelect = document.createElement("select");
        ZakaziForma.appendChild(SobaSelect);

        
        var input = document.createElement("input");
        input.type = "datetime-local";
        input.value = "2022-06-12T19:30";
        input.name = "datumkao";
        ZakaziForma.appendChild(input);

        var input2 = document.createElement("input");
        input2.type = "datetime-local";
        input2.value = "2030-66-6T19:30";
        input2.name = "datumkaodrugi";
        ZakaziForma.appendChild(input2);

        var button = document.createElement("button");
        button.innerHTML = "KLIKNI";
        ZakaziForma.appendChild(button);


     

        button.onclick = (ev) => this.Korisnik.Upisi(input.value, input2.value, SobaSelect.value);
        var listaZakazivanja = document.createElement("div");
        host.appendChild(listaZakazivanja);

        fetch("https://localhost:5001/Prenociste/GetPrenocista").then(
            response => {
            if(response.ok)
            {
                response.json().then( prena => {
                
                    prena.forEach(pren => {
                        var sel = document.createElement("option");
                        sel.value = pren.id;
                        sel.innerHTML = pren.naziv;
                    
                        SpcSelect.appendChild(sel);    
                     
                    });
                    this.crtajSobe(host,ZakaziForma, SpcSelect.value, SobaSelect, listaZakazivanja);
                    SpcSelect.onchange = (ev) => this.crtajSobe(host,ZakaziForma, SpcSelect.value, SobaSelect, listaZakazivanja);
                } );

            }
            else
            {
             
                alert("Doslo je do greske sa prenocistima");
            }
        })

    }
        

    crtajSobe(host, ZakaziForma, prenocisteid, SobaSelect, listaZakazivanja)
    {
        
        this.cleanPage(SobaSelect);
        fetch("https://localhost:5001/Soba/GetSobe/"+prenocisteid).then(
            response => {
            if(response.ok)
            {

               var nizSoba = []
                var x;  
                response.json().then( sobe => {
                    
                    sobe.forEach(soba => {
                        var option = document.createElement("option");
                        option.value = soba.id;
                        option.innerHTML = soba.naziv;
                    
                        SobaSelect.appendChild(option);    
                         x = new Soba(soba.id, soba.naziv);  
                        
                         nizSoba.push(x);
                    });
                    nizSoba[0].Crtaj(listaZakazivanja);
                    SobaSelect.onchange = (ev) => {
                        nizSoba.forEach(Soba => {
                            if(Soba.ID == SobaSelect.value)
                            {
                                Soba.Crtaj(listaZakazivanja);
                            }
                        })
                    };
                 


                } );
            }
       });



    }

    

    logout(btnLog, host, div1, div2)
    {

        this.Korisnik = null;
        this.username = null;
        this.password = null;
        btnLog.innerHTML = "Login";
        btnLog.onclick = (ev) => this.crtajLogin(btnLog);
        var nav = div1.parentNode;
        nav.removeChild(div1);
        nav.removeChild(div2);
    }


    cleanPage(host) 
    {
        if(typeof(host.lastElementChild) == "undefined" || host.lastElementChild == null) 
        {
            return;
        }
        
        while (host.lastElementChild)
        {
            
            host.removeChild(host.lastElementChild);
        }
    }
   

}

var main = new Main();
main.Crtaj();