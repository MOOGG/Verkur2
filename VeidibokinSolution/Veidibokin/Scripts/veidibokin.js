    function ChangeSize() { 
        var tBox = document.getElementById('<%=TextBox1.ClientID%>')
        tBox.style['width'] = ((tBox.value.length)*8) + 'px'
    }
