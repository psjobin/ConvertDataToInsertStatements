
function Process() {
    try {
        document.getElementById("resultData").value = ""
        var jsonData = document.getElementById("taTableData").value;
        var a = JSON.parse(jsonData).headers
        for (key in a) {
            var statement = ""
            statement += "insert into " + key + "("
            for (x in a[key]) {
                statement += a[key][x] + ","
            }
            statement = statement.slice(0, -1)
            statement += ") values ("
            var rows = JSON.parse(jsonData).rows
            for (key2 in rows[key]) {
                for (val in rows[key][key2]) {
                    statement += "'" + rows[key][key2][val] + "',"
                }
                statement = statement.slice(0, -1)
                statement += "),("
            }
            statement = statement.slice(0, -2)
            statement += ";\n\n"
            document.getElementById("resultData").value += statement;
        }
    } catch (error) {
        document.getElementById("resultData").value += "Something went wrong... make sure your json is good!";
        alert("something went wrong - make sure your json is good!")
    }


}