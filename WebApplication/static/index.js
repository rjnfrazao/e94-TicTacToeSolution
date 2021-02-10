document.addEventListener("DOMContentLoaded", function () {
  initialize_game();
});

is_game_in_progressing = false;
is_computer_playing = false;
player_symbol = "";
azure_symbol = "";
who_plays_first = "";
api_url = "";

//
//  Function to initialize the application
//
function initialize_game() {
  // Hide the error message
  //document.querySelector("#error-message").innerHTML = "";
  document.querySelector("div.game_options").style.display = "none"; // Hide game options
    document.querySelector("div.game_board").style.display = "none";  // Hide game board
  // Welcome message
  alert("Welcome to Tic Tac Toe Game");
}


// Show the game options <div> (or "Form")
function show_game_options() {
  if (who_plays_first != "") {
    document.querySelector("#id_play_first").value = who_plays_first;
    document.querySelector("#id_symbol").value = player_symbol;
  }

  if (is_game_in_progressing) {
    alert("Game in progress, wait the game finish.");
    return;
  } else {
    document.querySelector("div.game_options").style.display = "block";
  }
}

// Hide the div with the form to change the configuration.
function hide_game_options() {
  document.querySelector("div.game_options").style.display = "none";
}

// Change the game options.
function change_game_options() {
  if (is_game_in_progressing) {
    alert("Game in progress, wait the game finish.");
    return;
  } else {
    who_plays_first = document.querySelector("#id_play_first").value;
    player_symbol = document.querySelector("#id_symbol").value;
    if ((player_symbol == "X")) {
      azure_symbol = "O";
    } else {
      azure_symbol = "X";
    }
    api_url = document.querySelector("#api_url").value;
      document.querySelector("div.game_options").style.display = "none";
      document.querySelector("div.game_board").style.display = "block";
  }
}

// Start a new game, function called by the link Start New Game.
// The function requires that the user has selected the configuration
// and there are no game in progress.
function start_new_game() {
  // initiate variable
    if (who_plays_first == "") {
        alert("Please, define the game options first.");
        return;
    };

    alert("Get ready! A new game will start. Bobby Fischer coded the computer AI.");
    clear_game_board();
    is_game_in_progressing = true;
    document.querySelector("div.game_options").style.display = "none";
    document.querySelector("div.game_board").style.display = "block";
    document.querySelector("#board_header").innerText = "";

    if (who_plays_first == "azure") {
        alert("Before I forget! You are so kind, computer starts playing.")
        execute_move();               // Computer plays first
    }

}



// return the game board as array of chars.
function get_game_board() {
  game_board = [];
  elements = document.querySelectorAll(".board_cell");
  elements.forEach((element) => {
    position = parseInt(element.id.charAt(5));
    if (element.innerText == "") {
      game_board[position] = "?";
    } else {
      game_board[position] = element.innerText;
    }
  });
  return game_board;
}



// Clear the game board on the page.
function clear_game_board() {

    elements = document.querySelectorAll(".board_cell");
    elements.forEach((element) => {
        element.innerText = ""
    });
}



// Execute the move based on the position clicked
// input : id - position on the board. [0..8]
function execute_move(id) {
  // check if the game options was defined.
    if (who_plays_first == "") {
        alert("Please, define the options of the game first.");
        return;
    } 

    // check if the game options was defined.
    if (!is_game_in_progressing) {
        alert("Hey. Pay Attention. Start a new game first. The Link is on the top right.");
        return;
    } 

    if ((id>=0) && (id<=8)) {   
    // request came from the board in the screen, in case computer plays first these lines are skipped.

        // find the element clicked
        element_id = "#cell_" + id;
        element = document.querySelector(element_id);

        // the cell must be empty
        if (element.innerText == "") {
            element.innerText = player_symbol;
        } else {
            return; // does nothing
        }
    };

    console.log("Start a round. move is " + id)
    move_result = submitMove(id);                       // call the function resposible to fetch the server API

 
}

async function submitMove(move_id) {
    game_board = get_game_board();
    if ((move_id >= 0) && (move_id <= 8)) {
        bodyJson = JSON.stringify({
            move: move_id,
            azurePlayerSymbol: azure_symbol,
            humanPlayerSymbol: player_symbol,
            gameBoard: game_board,
        });
    } else {
        bodyJson = JSON.stringify({
            move: null,
            azurePlayerSymbol: azure_symbol,
            humanPlayerSymbol: player_symbol,
            gameBoard: game_board,
        });
    }


    console.log("** API post : " + bodyJson);
    is_computer_playing = true;

    // Fetch the api
    const response = await fetch(api_url, {
    method: 'POST',
        headers: { 'Content-Type': 'application/json'}, 
    body: bodyJson,
    });

    if (response.ok) {
      // response ok, return the JSON response.
      const move_json = await response.json();
      console.log("** API response -> " + JSON.stringify(move_json));
      await update_game_board(move_json);           // Update the game board.
 
  } else {

      console.log(response);
      alert("TILT TILT TILT. Game has to reset. I won't get extra Credit. HTTP status:" + response.status);
  }

}



// Update the game board on the page with the game board passed. 
// input - move_result - Json with the move.
function update_game_board(move_result) {

    // loop all elements
    for (i = 0; i < 9; i++) {
        element = document.querySelector("#cell_" + i)
        if (move_result.gameBoard[i] != "?") {
            element.innerText = move_result.gameBoard[i];
        } else {
            element.innerText = "";
        }             
    }

    // Evaluate if there is a winner or tie.
    is_game_in_progressing = false;                     // by default assume the game may have finished.
    element = document.querySelector("#board_header");

    // Information of the winner, tie, or inconclusive.
    winner = move_result.winner;
    if (winner == player_symbol) {
        // player won
        element.innerText = "Congratulations. You won. ";
    } else if (winner == azure_symbol) {
        // computer won
        element.innerText = "Such a dissapointment. Computer won.";
    } else if (winner == "tie") {
        // tie
        element.innerText = "Lazzy player. Tie.";
    } else {
        // Inconclusive. Game continues.
        is_game_in_progressing = true;
    }

    is_computer_playing = false;    // computer finish its move

}


