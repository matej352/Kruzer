# Kruzer

**Upute za stvaranje baze podataka:**
Potrebno imati instalirano postgresql, pgadmin na računalu.
Skripte za postavljanje baze podataka (**create_db.txt**) i inicijalno punjenje (**fill_db.txt**) nalaze se u repozitoriju.



**Upute za pokretanje Backend aplikacije KruzerApp:**
Potrebno imati instalirano .net6, visual studio 2022 na računalu.
Otvoriti projekt u Visual Studiu 2022 i dodati secrets.json sa sljedećim credentialsima za spajanje na bazu:

# Secrets.json
```ruby
{
  "ConnectionStrings": {
    "KruzerAppDb": "Server=localhost;Database=Kruzer;Port=port;User Id=postgres;Password=VasaLozinka"
  }
}
```

