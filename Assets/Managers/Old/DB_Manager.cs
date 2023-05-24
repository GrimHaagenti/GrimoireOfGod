using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;

[System.Serializable]
public class DB_Manager
{
    IDbConnection DB_Connection;

    private void OpenDatabase()
    {
        string DbUri = "URI=file:GrimoireOG_DB.sqlite";
        DB_Connection = new SqliteConnection(DbUri);
        DB_Connection.Open();

    }

    public void Init()
    {
        OpenDatabase();

    }

    public bool GetElementCombination(Elements element1, Elements element2, out Elements resultElement)
    {
        string query = "SELECT resultElement FROM elementCombination WHERE (element1 = \"" + element1.ToString() + "\" AND element2 = \"" + element2.ToString() + "\") OR (element1 = \"" + element2.ToString() + "\" AND element2 = \"" + element1.ToString() + "\") ; ";



        IDbCommand cmd = DB_Connection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        string result = "";

        while (dataReader.Read())
        {
            result = dataReader.GetString(0);
        }
        resultElement = Elements.FIRE;
        if (Enum.TryParse(typeof(Elements), result, out object res))
        {
            resultElement = (Elements)res;
            return true;
        }

        return false;
    }
}
