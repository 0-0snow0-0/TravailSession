/*Projet final Base de données / Interfaces Graphique

  Gabriel Porlier - 1564431
  Julianna Ferrucho - 2087801
*/

/* Création des tables */

    DROP TABLE IF EXISTS heuresTravaille;
    DROP TABLE IF EXISTS employes;
    DROP TABLE IF EXISTS projets;
    DROP TABLE IF EXISTS clients;
    DROP TABLE IF EXISTS admin;

    CREATE TABLE clients (
        id INT PRIMARY KEY,
        nom VARCHAR(50),
        adresse VARCHAR(100),
        numTel VARCHAR(20),
        email VARCHAR(20)
    );

    CREATE TABLE projets (
        numProjet VARCHAR(11) PRIMARY KEY,
        titre VARCHAR(100),
        dateDebut DATE,
        description VARCHAR(2000),
        budget INT,
        nbrEmpRequis INT,
        /*Peut être un estimé, ou un calcul total pour tout les employé de linked. A voir !*/
        totalSalaire DOUBLE,
        client INT,
        statut CHAR(8) DEFAULT 'En cours',

        CONSTRAINT FK_projet_client FOREIGN KEY (client) REFERENCES clients (id)
    );

    CREATE TABLE employes (
        matricule VARCHAR(10) PRIMARY KEY,
        nom VARCHAR(50),
        prenom VARCHAR(50),
        dateNaissance DATE,
        email VARCHAR(100),
        adresse VARCHAR(100),
        dateEmbauche DATE,
        tauxHoraire DOUBLE,
        photo_url VARCHAR(2000),
        statut VARCHAR(10) DEFAULT 'Journalier',
        numProjet VARCHAR(11),

        CONSTRAINT FK_employe_projet FOREIGN KEY (numProjet) REFERENCES projets (numProjet)
    );

    CREATE TABLE heuresTravaille (
        matricule VARCHAR(10),
        numProjet VARCHAR(11),
        nbrHeures DOUBLE DEFAULT 0,
        salaireEmploye DOUBLE DEFAULT 0,

        CONSTRAINT PK_heuresTravaille PRIMARY KEY (matricule, numProjet),
        CONSTRAINT FK_heuresTravaille_employe FOREIGN KEY (matricule) REFERENCES employes (matricule),
        CONSTRAINT FK_heuresTravaille_projet FOREIGN KEY (numProjet) REFERENCES projets (numProjet)
    );

    /*J'ai enleve username puisque je le trouvais redondant*/
    CREATE TABLE admin (
        username VARCHAR(100) PRIMARY KEY,
        password VARCHAR(100),
        firstBoot BOOLEAN
    );



/*CRÉATION DES PROCÉDURES D'INSERTION */
    DROP PROCEDURE IF EXISTS insert_client;

    DELIMITER //
    CREATE PROCEDURE insert_client(IN id_client INT, nom_client VARCHAR(50), adresse_client VARCHAR(100), numTel_client VARCHAR(20), email_client VARCHAR(20))
    BEGIN
        INSERT INTO clients VALUES (id_client, nom_client, adresse_client, numTel_client, email_client);
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS insert_projet;

    DELIMITER //
    CREATE PROCEDURE insert_projet(IN numProjet_projet VARCHAR(11), titre_projet VARCHAR(100), dateDebut_projet DATE, description_projet VARCHAR(2000), budget_projet INT, nbrEmpRequis_projet INT, totalSalaire_projet DOUBLE, client_projet INT, statut_projet CHAR(8))
    BEGIN
        INSERT INTO projets VALUES (numProjet_projet, titre_projet, dateDebut_projet, description_projet, budget_projet, nbrEmpRequis_projet, totalSalaire_projet, client_projet, statut_projet);
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS insert_employe;

    DELIMITER //
    CREATE PROCEDURE insert_employe(IN matricule_e VARCHAR(10), nom_e VARCHAR(50), prenom_e VARCHAR(50), dateNaissance_e DATE, email_e VARCHAR(100), adresse_e VARCHAR(100), dateEmbauche_e DATE, tauxHoraire_e DOUBLE, photo_url_e VARCHAR(2000), statut_e VARCHAR(10), numProjet_e VARCHAR(11))
    BEGIN
        INSERT INTO employes VALUES (matricule_e, nom_e, prenom_e, dateNaissance_e, email_e, adresse_e, dateEmbauche_e, tauxHoraire_e, photo_url_e, statut_e, numProjet_e);
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS insert_heure;

    DELIMITER //
    CREATE PROCEDURE insert_heure(IN matricule_h VARCHAR(10), numProjet_h VARCHAR(11), nbrHeure_h DOUBLE, salaireEmploye_h DOUBLE)
    BEGIN
        INSERT INTO heuresTravaille VALUES (matricule_h, numProjet_h, nbrHeure_h, salaireEmploye_h);
    end //
    DELIMITER ;

/*CREATION DES PROCEDURES DE MODIFICATIONS*/

    DROP PROCEDURE IF EXISTS modification_client;

    DELIMITER //
    CREATE PROCEDURE modification_client(IN id_client INT, nom_client VARCHAR(50), adresse_client VARCHAR(100), numTel_client VARCHAR(20), email_client VARCHAR(20))
    BEGIN
        UPDATE clients SET nom = nom_client, adresse = adresse_client, numTel = numTel_client, email = email_client WHERE id = id_client;
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS modification_employe;

    DELIMITER //
    CREATE PROCEDURE modification_employe(IN matricule_e VARCHAR(10), nom_e VARCHAR(50), prenom_e VARCHAR(50), dateNaissance_e DATE, email_e VARCHAR(100), adresse_e VARCHAR(100), dateEmbauche_e DATE, tauxHoraire_e DOUBLE, photo_url_e VARCHAR(2000), statut_e VARCHAR(10), numProjet_e VARCHAR(11))
    BEGIN
        IF statut_e != 'Permanent' THEN
            SET statut_e = null;
        end if ;

        UPDATE employes
        SET nom = nom_e, prenom = prenom_e, email = email_e, adresse = adresse_e, tauxHoraire = tauxHoraire_e, photo_url = photo_url_e, statut = statut_e, numProjet = numProjet_e
        WHERE matricule = matricule_e;
    end //
    DELIMITER ;


    /*POUR PROJET, peut changer titre, date_debut????, description, budget, nbrEmprequis, statut */
    DROP PROCEDURE IF EXISTS modification_projet;

    DELIMITER //
    CREATE PROCEDURE modification_projet(IN numProjet_projet VARCHAR(11), titre_projet VARCHAR(100), dateDebut_projet DATE, description_projet VARCHAR(2000), budget_projet INT, nbrEmpRequis_projet INT, statut_projet CHAR(7))
    BEGIN
        UPDATE projets
        SET titre = titre_projet, description = description_projet, budget = budget_projet, nbrEmpRequis = nbrEmpRequis_projet, statut = statut_projet
        WHERE numProjet = numProjet_projet;
    end //
    DELIMITER ;

    /*HEURES TRAVAILLE*/
    DROP PROCEDURE IF EXISTS modification_heure;

    DELIMITER //
    CREATE PROCEDURE modification_heure(IN matricule_h VARCHAR(10), numProjet_h VARCHAR(11), nbrHeures_h DOUBLE)
    BEGIN
        UPDATE heuresTravaille
        SET nbrHeures = nbrHeures_h, salaireEmploye = 0
        WHERE numProjet = numProjet_h AND matricule = matricule_h;
    end //
    DELIMITER ;

/*CREATION DES PROCEDURES DE SUPPRESSION*/

    /*CLIENT*/
    DROP PROCEDURE IF EXISTS supprimer_client;

    DELIMITER //
    CREATE PROCEDURE supprimer_client(IN id_client INT)
    BEGIN
        DELETE FROM clients WHERE id = id_client;
    end //
    DELIMITER ;

    /*EMPLOYE*/
    DROP PROCEDURE IF EXISTS supprimer_employe;

    DELIMITER //
    CREATE PROCEDURE supprimer_employe(IN matricule_employe VARCHAR(10))
    BEGIN
        DELETE FROM employes WHERE matricule = matricule_employe;
    end //
    DELIMITER ;

    /*PROJET*/
    DROP PROCEDURE IF EXISTS supprimer_projet;

    DELIMITER //
    CREATE PROCEDURE supprimer_projet(IN numProjet_projet VARCHAR(11))
    BEGIN
        DELETE FROM projets WHERE numProjet = numProjet_projet;
    end //
    DELIMITER ;

    /*HEURES*/
    DROP PROCEDURE IF EXISTS supprimer_heure;

    DELIMITER //
    CREATE PROCEDURE supprimer_heure(IN numProjet_heure VARCHAR(10), matricule_heure VARCHAR(11))
    BEGIN
        DELETE FROM heuresTravaille WHERE numProjet = numProjet_heure AND matricule = matricule_heure;
    end //
    DELIMITER ;

/*SETTING FIRST TIME BOOT ADMIN*/

    DROP PROCEDURE IF EXISTS activate_admin;

    DELIMITER //
    CREATE PROCEDURE activate_admin(IN email_a VARCHAR(100), password_a VARCHAR(50))
    BEGIN
        UPDATE admin
        SET username = email_a, password = password_a, firstBoot = true
        WHERE firstBoot = false;
    end //
    DELIMITER ;

/*CREATION DES PROCEDURES D'AFFICHAGES*/

    /*CLIENTS*/
    DROP PROCEDURE IF EXISTS show_all_clients;

    DELIMITER //
    CREATE PROCEDURE show_all_clients()
    BEGIN
        SELECT * FROM clients;
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS show_client;

    DELIMITER //
    CREATE PROCEDURE show_client(IN id_c INT)
    BEGIN
        SELECT * FROM clients
        WHERE id = id_c;
    end //
    DELIMITER ;

    /*PROJET*/
    DROP PROCEDURE IF EXISTS show_all_projets;

    DELIMITER //
    CREATE PROCEDURE show_all_projets()
    BEGIN
        SELECT * FROM projets;
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS show_projet;

    DELIMITER //
    CREATE PROCEDURE show_projet(IN numProjet_p VARCHAR(11))
    BEGIN
        SELECT * FROM projets
        WHERE numProjet = numProjet_p;
    end //
    DELIMITER ;

    /*EMPLOYE*/
    DROP PROCEDURE IF EXISTS show_all_employes;

    DELIMITER //
    CREATE PROCEDURE show_all_employes()
    BEGIN
        SELECT * FROM employes;
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS show_employe;

    DELIMITER //
    CREATE PROCEDURE show_employe(IN matricule_e VARCHAR(10))
    BEGIN
        SELECT * FROM employes
        WHERE matricule = matricule_e;
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS show_employes_for_project;

    DELIMITER //
    CREATE PROCEDURE show_employes_for_project(IN in_numProjet VARCHAR(11))
    BEGIN
        SELECT * FROM employes WHERE matricule IN (SELECT matricule
                                                   FROM heurestravaille
                                                   WHERE heurestravaille.numProjet = in_numProjet);
    end //
    DELIMITER ;

    /*HEURETRAVAILLE*/
    DROP PROCEDURE IF EXISTS show_all_heures;

    DELIMITER //
    CREATE PROCEDURE show_all_heures()
    BEGIN
        SELECT * FROM heuresTravaille;
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS show_all_heures_pour_projet;

    DELIMITER //
    CREATE PROCEDURE show_all_heures_pour_projet(IN in_numProjet VARCHAR(11), in_matricule VARCHAR(10))
    BEGIN
        SELECT * FROM heurestravaille WHERE numProjet = in_numProjet AND matricule = in_matricule;
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS show_heure;

    DELIMITER //
    CREATE PROCEDURE show_heure(IN matricule_h VARCHAR(10), numProjet_h VARCHAR(11))
    BEGIN
        SELECT * FROM heuresTravaille
        WHERE matricule = matricule_h AND numProjet = numProjet_h;
    end //
    DELIMITER ;

    /*ADMIN*/
    DROP PROCEDURE IF EXISTS show_admin;

    DELIMITER //
    CREATE PROCEDURE show_admin()
    BEGIN
        SELECT username FROM admin;
    end //
    DELIMITER ;

/*CREATION DE TRIGGERS*/
    /*TRIGGER qui définit le matricule des employé*/

    DROP TRIGGER IF EXISTS before_insert_employe;

    DELIMITER //
    CREATE TRIGGER before_insert_employe
    BEFORE INSERT ON employes
    FOR EACH ROW
        BEGIN
            SET new.matricule = CONCAT(UPPER(SUBSTRING(new.nom, 1, 2)), '-', YEAR(new.dateNaissance), '-', generate_10_99());
        end //
    DELIMITER ;

    /*TRIGGERS CLIENTS*/
    DROP TRIGGER IF EXISTS before_insert_client;

    DELIMITER //
    CREATE TRIGGER before_insert_client
    BEFORE INSERT ON clients
    FOR EACH ROW
        BEGIN
            DECLARE u BOOLEAN;
            DECLARE i INT;

            SET u = false;


            WHILE !u DO
                SET i = generate_100_999();

                IF i NOT IN (SELECT id FROM clients) THEN
                    SET u = true;
                end if ;
            end while ;

            SET new.id = i;
        end //
    DELIMITER ;

    /*TRIGGER EMPLOYE*/
    DROP TRIGGER IF EXISTS before_update_employes;

    DELIMITER //
    CREATE TRIGGER before_update_employes
    BEFORE UPDATE ON employes
    FOR EACH ROW
        BEGIN
            IF new.statut IS NOT null THEN
                IF YEAR(old.dateEmbauche) - YEAR(NOW()) < 3 THEN
                    SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Le statut d\'employé ne peux pas être changé avant 3 ans d\'ancienneté';
                end if ;
            end if ;

            IF new.numProjet != old.numProjet THEN
                IF (SELECT statut FROM projets WHERE numProjet = old.numProjet) != 'Terminé' THEN
                    SIGNAL SQLSTATE '45000'
                    SET MESSAGE_TEXT = 'Le projet actif de l\'employé ne peut pas être changé puisque celui actuel n\'est pas terminé.';
                end if ;
            end if ;
        end //
    DELIMITER ;

    /*TRIGGERS PROJETS*/
    DROP TRIGGER IF EXISTS before_insert_projet;

    DELIMITER //
    CREATE TRIGGER before_insert_projet
    BEFORE INSERT ON projets
    FOR EACH ROW
        BEGIN
            DECLARE r INT;
            DECLARE s VARCHAR(2);
            SET r = generate_01_99();

            /*A VOIR, PEUT être que changement de int à string est non automatique*/
            IF r < 10 THEN
                SET s = CONCAT('0', r);
            ELSE
                SET s = r;
            end if ;

            SET new.numProjet = CONCAT(new.client, '-', s, '-', YEAR(new.dateDebut));

        end //
    DELIMITER ;

    /*
    DROP TRIGGER IF EXISTS before_update_projet;

    DELIMITER //
    CREATE TRIGGER before_update_projet
    BEFORE INSERT ON projets
    FOR EACH ROW
        BEGIN
            DECLARE r INT;
            DECLARE s VARCHAR(2);
            SET r = generate_01_99();


            IF r < 10 THEN
                SET s = CONCAT('0', r);
            ELSE
                SET s = r;
            end if ;

            SET new.numProjet = CONCAT(new.client, '-', s, '-', YEAR(new.dateDebut));

        end //
    DELIMITER ;
    */

    /*HEURES TRAVAILLE*/
    DROP TRIGGER IF EXISTS before_insert_heures;

    DELIMITER //
    CREATE TRIGGER before_insert_heures
    BEFORE INSERT ON heuresTravaille
    FOR EACH ROW
        BEGIN
            SET new.salaireEmploye = ROUND(calcul_salaire(matricule, numProjet, 0), 2);
        end //
    DELIMITER ;

    DROP TRIGGER IF EXISTS before_update_heures;

    DELIMITER //
    CREATE TRIGGER before_update_heures
    BEFORE UPDATE ON heuresTravaille
    FOR EACH ROW
        BEGIN
            DECLARE s_total DOUBLE;

            SET new.salaireEmploye = ROUND(calcul_salaire(new.matricule, new.numProjet, new.nbrHeures), 2);
        end //
    DELIMITER ;

    DROP TRIGGER IF EXISTS after_update_heures;

    DELIMITER //
    CREATE TRIGGER after_update_heures
    AFTER UPDATE ON heurestravaille
    FOR EACH ROW
        BEGIN
            CALL update_salaire_total(new.matricule, new.numProjet);
        end //
    DELIMITER ;

/*ROUTINES SPÉCIFIQUES*/
    DROP FUNCTION IF EXISTS calcul_salaire;

    DELIMITER //
    CREATE FUNCTION calcul_salaire( i_matricule VARCHAR(10), i_numProjet VARCHAR(11), i_heures DOUBLE) RETURNS DOUBLE
    BEGIN
        DECLARE salaire DOUBLE;

        SET salaire = ROUND(i_heures * (SELECT tauxHoraire FROM employes WHERE matricule = i_matricule), 2);
        RETURN salaire;
    end //
    DELIMITER ;


    DROP PROCEDURE IF EXISTS update_salaire_total;

    DELIMITER //
    CREATE PROCEDURE update_salaire_total( i_matricule VARCHAR(10), i_numProjet VARCHAR(11))
    BEGIN
        DECLARE salaire DOUBLE;

        SET salaire = ROUND((SELECT SUM(salaireEmploye) FROM heurestravaille ht WHERE ht.numProjet = i_numProjet AND ht.matricule = i_matricule), 2);

        UPDATE projets
        SET totalSalaire = salaire
        WHERE numProjet = i_numProjet;
    end //
    DELIMITER ;


    /*ASSIGNATION D'UN EMPLOYÉ À UN PROJET*/

    DROP PROCEDURE IF EXISTS link_employe_projet;

    DELIMITER //
    CREATE PROCEDURE link_employe_projet(IN i_matricule VARCHAR(10), i_numProjet VARCHAR(11))
        BEGIN
            UPDATE employes
            SET numProjet = i_numProjet
            WHERE matricule = i_matricule;

            CALL insert_heure(i_matricule, i_numProjet, 0, 0);
        end //
    DELIMITER ;

    /*Functions pour générer un int aléatoire*/
    DROP FUNCTION IF EXISTS generate_01_99;

    DELIMITER //
    CREATE FUNCTION generate_01_99() RETURNS INT
    BEGIN
        DECLARE i INT;
        SET i = FLOOR(RAND()*(99-1)+1);

        RETURN i;
    end //

    DROP FUNCTION IF EXISTS generate_10_99;

    DELIMITER //
    CREATE FUNCTION generate_10_99() RETURNS INT
    BEGIN
        DECLARE i INT;
        SET i = FLOOR(RAND()*(99-10)+10);

        RETURN i;
    end //

    DROP FUNCTION IF EXISTS generate_100_999;

    DELIMITER //
    CREATE FUNCTION generate_100_999() RETURNS INT
    BEGIN
        DECLARE i INT;
        SET i = FLOOR(RAND()*(999-100)+100);

        RETURN i;
    end //

    DROP PROCEDURE IF EXISTS show_projets_en_cours;

    DELIMITER //
    CREATE PROCEDURE show_projets_en_cours()
    BEGIN
        SELECT * FROM projets WHERE statut = 'En cours';
    end //
    DELIMITER ;

    DROP PROCEDURE IF EXISTS show_projets_termine;

    DELIMITER //
    CREATE PROCEDURE show_projets_termine()
    BEGIN
        SELECT * FROM projets WHERE statut = 'Terminé';
    end //
    DELIMITER ;



