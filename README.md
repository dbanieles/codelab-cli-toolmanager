## CodeLab ToolManager CLI

Gestore CLI per la gestione di tool come Terraform e Java, sviluppato in C#/.NET. Permette di installare, elencare, selezionare versioni, gestire alias e disinstallare tool in modo semplice e automatizzato.

---

### Struttura del Progetto

- **CodeLab.ToolManager.Cli.Java/**: CLI per la gestione di Java (placeholder, da implementare)
- **CodeLab.ToolManager.Cli.Terraform/**: CLI per la gestione di Terraform
	- `Commands/`: Comandi disponibili (install, list, use, uninstall, set-alias, unset-alias)
	- `appsettings.json`: Configurazione percorso, repo e modalità installazione
- **CodeLab.ToolManager.Pkg.Core/**: Logica core condivisa, servizi, modelli e client

---

### Funzionalità Principali (Terraform)

- **Installazione versione**: `install <version>`
- **Elenco versioni installate**: `list`
- **Selezione versione da usare**: `use <version>`
- **Disinstallazione versione**: `uninstall <version>`
- **Gestione alias**: `set-alias <aliasName>`, `unset-alias <aliasName>`

---

### Configurazione

Modifica `appsettings.json` per personalizzare:

```json
{
	"Terraform": {
		"RepositoryBaseUrl": "https://releases.hashicorp.com/terraform/",
		"InstallPath": "C:\\Tools\\Terraform",
		"InstallName": "TF_HOME",
		"InstallMode": "User"
	}
}
```

---

### Build e Avvio

1. **Build**:
	 - Apri terminale nella root del progetto
	 - Esegui: `dotnet build CodeLab.ToolManager.Cli.Terraform/CodeLab.ToolManager.Cli.Terraform.csproj`

2. **Esecuzione**:
	 - Esegui: `dotnet run --project CodeLab.ToolManager.Cli.Terraform/CodeLab.ToolManager.Cli.Terraform.csproj -- <comando>`
	 - Esempio: `dotnet run --project ... -- install 1.6.0`

---

### Esempi di Utilizzo

```sh
# Installa una versione
dotnet run --project CodeLab.ToolManager.Cli.Terraform -- install 1.6.0

# Elenca versioni installate
dotnet run --project CodeLab.ToolManager.Cli.Terraform -- list

# Usa una versione
dotnet run --project CodeLab.ToolManager.Cli.Terraform -- use 1.6.0

# Disinstalla una versione
dotnet run --project CodeLab.ToolManager.Cli.Terraform -- uninstall 1.6.0

# Imposta un alias
dotnet run --project CodeLab.ToolManager.Cli.Terraform -- set-alias latest

# Rimuovi un alias
dotnet run --project CodeLab.ToolManager.Cli.Terraform -- unset-alias latest
```

---

### Contribuire

1. Forka il repository
2. Crea un branch feature: `git checkout -b feature/nome-feature`
3. Fai commit e push delle modifiche
4. Apri una Pull Request

---

### Licenza

MIT
