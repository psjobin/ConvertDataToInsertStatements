
function Process(){
    var jsonData = document.getElementById("taTableData").value; 
    var a = JSON.parse(JSON.stringify(JSON.parse( jsonData).headers))
    //console.log(a); 
    var statement = ""
    for(key in a){
        //console.log(key, '->' , a[key])
        statement += "insert into " + key + "("
        for(x in a[key]){
            //console.log(a[key][x])
            statement += a[key][x] + ","
        }
        statement = statement.slice(0,-1)
        statement += ");"
        console.log(statement);
    }

}