
function Process(){
    var jsonData = document.getElementById("taTableData").value; 
    var a = JSON.parse(JSON.stringify(JSON.parse( jsonData).headers))
    //console.log(a); 
    for(key in a){
        var statement = ""
        //console.log(key, '->' , a[key])
        statement += "insert into " + key + "("
        for(x in a[key]){
            //console.log(a[key][x])
            statement += a[key][x] + ","
        }
        statement = statement.slice(0,-1)
        statement += ") values ("

        var rows = JSON.parse( jsonData).rows
       // console.log(rows[key])
            for(key2 in rows[key]){
                //console.log(rows[key][key2])
                for(val in rows[key][key2]){
                    //console.log(rows[key][key2][val])
                    statement += "'" + rows[key][key2][val] + "',"
                }
                statement = statement.slice(0,-1)
                statement += "),("
            }
            statement = statement.slice(0,-2)
            statement += ";"
            console.log(statement);
    }
}