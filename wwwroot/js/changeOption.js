function ChangeOption(id) {

    let option = document.querySelectorAll("#options");
    let optionCount = option.length;

    for (let index = 0; index < optionCount; index++) {
       option[id].hidden = false;
       if (id != index) {
         option[index].hidden = true;
       }

    }

}
