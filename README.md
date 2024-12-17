# SportSync - Application de Gestion Sportive

SportSync est une application web moderne construite avec ASP.NET Core qui aide à gérer les activités sportives, les inscriptions et les sessions utilisateurs. Elle offre une expérience fluide tant pour les athlètes que pour les administrateurs.

## Fonctionnalités

- **Authentification Utilisateur**
  - Système sécurisé de connexion et d'inscription
  - Gestion des profils utilisateurs
  - Contrôle d'accès basé sur les rôles (Administrateur, Entraîneur, Utilisateur)

- **Gestion des Sports**
  - Parcourir différentes disciplines sportives
  - Voir les informations détaillées de chaque sport
  - Filtrer les sports par type, niveau et exigences techniques

- **Gestion des Sessions**
  - Planifier et gérer les séances d'entraînement
  - Suivre les présences
  - Gérer les inscriptions

- **Système de Paiement**
  - Traitement sécurisé des paiements
  - Support de plusieurs méthodes de paiement
  - Suivi de l'historique des paiements

- **Gestion des Documents**
  - Télécharger et gérer les certificats médicaux
  - Stocker les documents importants
  - Suivre le statut des documents

## Stack Technologique

- **Backend**
  - ASP.NET Core 7.0
  - Entity Framework Core
  - Base de données SQL Server
  - Identity Framework pour l'authentification

- **Frontend**
  - Vues Razor
  - Bootstrap 5
  - jQuery
  - Design responsive moderne

## Pour Commencer

1. **Prérequis**
   - SDK .NET 7.0
   - SQL Server
   - Visual Studio 2022 ou VS Code

2. **Installation**
   ```bash
   # Cloner le dépôt
   git clone https://github.com/asmaabrs97/SportSync.git

   # Naviguer vers le répertoire du projet
   cd SportSync

   # Restaurer les packages
   dotnet restore

   # Mettre à jour la base de données
   dotnet ef database update

   # Lancer l'application
   dotnet run
   ```

3. **Configuration**
   - Mettre à jour la chaîne de connexion dans `appsettings.json`
   - Configurer les paramètres supplémentaires dans `appsettings.json`

## Structure du Projet

- `Controllers/` - Contrôleurs de l'application
- `Models/` - Modèles de données et modèles de vue
- `Views/` - Vues Razor et layouts
- `Services/` - Logique métier et services
- `Data/` - Contexte de base de données et repositories
- `wwwroot/` - Fichiers statiques (CSS, JS, images)

## Comptes de test

L'application crée automatiquement deux comptes pour les tests lors du premier démarrage :

### Compte Administrateur
- Email : admin@sportsync.com
- Mot de passe : Admin123!
- Rôle : Admin
- Accès : Dashboard administrateur, gestion des utilisateurs, gestion des sports, etc.

### Compte Utilisateur Test
- Email : aya.dyr@example.com
- Mot de passe : Test123!
- Rôle : User
- Accès : Inscription aux sports, gestion du profil, paiements, etc.

Ces comptes sont créés automatiquement lors du premier démarrage de l'application. Vous pouvez les utiliser immédiatement après avoir :
1. Cloné le projet
2. Exécuté les migrations (`dotnet ef database update`)
3. Lancé l'application (`dotnet run`)

## Fonctionnalités en Développement

- Fonctionnalité de recherche avancée
- Notifications en temps réel
- Application mobile
- Intégration avec des API sportives externes
- Tableau de bord d'analyse des performances

## Contribuer

1. Forker le dépôt
2. Créer votre branche de fonctionnalité (`git checkout -b feature/NouvellefonctionnalitéIncroyable`)
3. Commiter vos changements (`git commit -m 'Ajout d'une nouvelle fonctionnalité incroyable'`)
4. Pousser vers la branche (`git push origin feature/NouvellefonctionnalitéIncroyable`)
5. Ouvrir une Pull Request

## Licence

Ce projet est sous licence MIT. Voir le fichier `LICENSE` pour plus de détails.

## Contact

Lien du Projet : [https://github.com/asmaabrs97/SportSync](https://github.com/asmaabrs97/SportSync)
