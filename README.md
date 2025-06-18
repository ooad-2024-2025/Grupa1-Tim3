# Grupa1-Tim3: Nutritionist Web App

**Live demo:**  
👉 https://nutritionist-app-d5eseqbycheydeeu.westeurope-01.azurewebsites.net/  

Aplikacija je deployana na besplatnu (Free) App Service planu na Azure.

---

## 🔑 Kredencijali

| Uloga           | Korisničko ime | Lozinka       |
| --------------- | -------------- | ------------- |
| **Admin**       | `Jhafo`        | `Password123!` |
| **Nutricionista** | `nutricionista` | `Password123!` |

---

## 🗄️ Baza podataka

- **Connection string:** Server=tcp:ooadtim3.database.windows.net,1433;Initial Catalog=nutritionist;Persist Security Info=False;User ID=ooadtim3;Password=EnsarNejraAdiAjdinTim3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30; 
- **Tip:** Azure SQL Serverless (Free), sve IP adrese dozvoljene
- **Behavior:** serverless baza se nakon 1h neaktivnosti pauzira

### Budenje baze

Prvi poziv na bilo koju akciju koja koristi bazu može potrajati **1–2 minute** (timeout zamka), dok se baza “probudI”. Nakon buđenja, svi zahtjevi prolaze brzo.

VAZNA NAPOMENA: Baza koja se koristi jeste free serverless baza na azure servisima, koja se nakon 1h neaktivnosti pauzira da se ne bi potrošila. Ukoliko zelite koristiti aplikaciju bilo to na deployanom linku ili na lokalnoj mašini na prvom pozivu koji ima nešto vezano za bazu će loadati 1-2minute i timeoutati. Tada ce se baza "probuditi" i onda ce sve raditi kako treba.
---

## ⚠️ Važne napomene

1. **Newsletter lokalno**  
   Za slanje newslettera lokalno zatražite **SendGrid API key** od člana tima (e-mail: `edzafo1@etf.unsa.ba`).  
   > Github-ov secret-scanner automatski briše javno pusheane API ključeve, pa ih ne možemo držati u repozitoriju.  

VAZNA NAPOMENA 2: Ukoliko zelite koristiti newsletter na lokalnoj mašini, zamolit cu vas da tražite api ključ od člana tima na email "edzafo1@etf.unsa.ba". Razlog tome jeste ako pusham-o na public repository api ključ od send grid-a oni automatski izbrišu taj ključ i on je nevalidan.

---

> _Hvala što koristite našu Nutritionist aplikaciju!_  
