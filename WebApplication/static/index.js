document.addEventListener("DOMContentLoaded", function () {
  // Use buttons to toggle between views
  /*  document
    .querySelector("#post-form-submit")
    .addEventListener("click", upd_post); // event to open inbox mailbox
*/

  initialize_game();
});

is_game_in_progressing = false;
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
    api_url = document.querySelector("#api_url").value;
    document.querySelector("div.game_options").style.display = "none";
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
  }

  if (is_game_in_progressing) {
    alert("Please, wait. We still have one game in progress.");
    return;
  }
}

// retrun the game board in array of chars.
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

// Execute the move based on the position clicked
// input : id - position on the board. [0..8]
function execute_move(id) {
  // check if the game options was defined.
  if (who_plays_first == "") {
    alert("Please, define the game options first.");
    return;
  }

  // find the element clicked
  element_id = "#cell_" + id;
  element = document.querySelector(element_id);

  // the cell must be empty
  if (element.innerText == "") {
    element.innerText = player_symbol;
  } else {
    return; // does nothing
  }

  move = submitMove(id);
  alert("Move Status = " & move.winner);
}

async function submitMove(move_id) {
  game_board = get_game_board();
  alert(game_board);

  const response = await fetch(api_url, {
    method: "POST",
    headers: { mode: "no-cors" },
    body: JSON.stringify({
      move: move_id,
      azurePlayerSymbol: azure_symbol,
      humanPlayerSymbol: player_symbol,
      gameBoard: game_board,
    }),
  });

  if (response.ok) {
    const move = await response.json();
    return move;
  } else {
    alert("TILT in Tic Tac Toe. Game has to reset" + response.status);
  }

  event.preventDefault();
}
/*
  // if user liked or net yet the button displayed changes
  if (owner_liked) {
    // user logged already liked this post
    btn_liked = `<input type="button" class="btn btn-success btn-sm" onclick="upd_like(${id_post}, 'unliked')" value="I liked">`;
  } else {
    // user logged hasn´t liked this post yet
    btn_liked = `<input type="button" class="btn btn-outline-secondary btn-sm" onclick="upd_like(${id_post}, 'liked')" value="Likes?">`;
  }

  return btn_liked;
}

//
// Function to render the like button, without need to refresh the screen.
// Parameters : owner_liked - inform if owner like or not the post, if liked display unlike button, otherwise like button
//              id_post - post to have the button refreshed.
//
function render_btn_liked(owner_liked, id_post) {
  // initiate variable
  btn_liked = "";

  // if user liked or net yet the button displayed changes
  if (owner_liked) {
    // user logged already liked this post
    btn_liked = `<input type="button" class="btn btn-success btn-sm" onclick="upd_like(${id_post}, 'unliked')" value="I liked">`;
  } else {
    // user logged hasn´t liked this post yet
    btn_liked = `<input type="button" class="btn btn-outline-secondary btn-sm" onclick="upd_like(${id_post}, 'liked')" value="Likes?">`;
  }

  return btn_liked;
}

//
// Function to render temporaly the form to update the post.
// Parameters : div_id - div where form will be inserted, id_post - post to be edited
//
function frm_post(div_id, id_post) {
  // initialize temp variables
  message = document.querySelector(div_id).innerHTML;

  frm = `
    <form id="edit-form">
      <div class="form-group">
          <textarea class="form-control" id="edit-message" placeholder="Body" rows="5">${message}</textarea>
          <input type="hidden" id="id_post" value="${id_post}">
          <input type="button" id="post-form-submit" onClick="upd_post(${id_post})" class="btn btn-primary" value="Edit">
      </div>
    </form>  
  `;
  document.querySelector(`#post-item-btnedit-${id_post}`).style.display =
    "none";
  document.querySelector(div_id).innerHTML = frm;
}

//
// Function to render the navigation page bar
// Parameter : dict - page dictionary to get information in each page we are and total count.
//             username - parameter require to render load_posts function - used to return posts from a user
//             following - flag required to render load_posts funtion - used to reutrn posts from followed users
//
function html_page_navigation(dict, username, following) {
  // initialize temp variables
  let previous = "";
  let next = "";

  // check if previous button is required.
  if (dict["previous"] != -1) {
    previous = `<input type="button" class="btn btn-outline-secondary btn-sm btn-block" onclick="load_posts('${username}',${following},${dict["previous"]})" value="Previous">`;
  }

  // check if next button is required.
  if (dict["next"] != -1) {
    next = `<input type="button" class="btn btn-outline-secondary btn-sm btn-block" onclick="load_posts('${username}',${following},${dict["next"]})" value="Next">`;
  }

  // render all elements.
  let nav_pagination = `
  <div class="box">
    <div>
      <div class="row">
        <div class="col-2">${previous}</div>
        <div class="col-1"></div>
        <div class="col-2">${next}</div>
        <div class="col-7"></div>
      </div>
  </div>`;

  return nav_pagination;
}

//
//  Function Responsible to render the page to display all posts
//
function allpost() {
  // Hide the error message
  document.querySelector("#error-message").innerHTML = "";
  document.querySelector("#error-message").style.display = "none";

  // load all post messages
  load_posts("", false, 0);
}

//
//  Function Responsible to render the page to display Following
//
function following() {
  // Hide the error message
  document.querySelector("#error-message").innerHTML = "";
  document.querySelector("#error-message").style.display = "none";

  // load all post messages
  load_posts("", true, 0);
}

//
//  Function Responsible post a new comment to the user.
//  Function invoke from the button post of the form.
//
function upd_post(id_post) {
  if (id_post == 0) {
    fetch_url = "/save_post";
    method = "POST";
    message = document.querySelector("#message").value;
  } else {
    fetch_url = "/upd_post";
    method = "PUT";
    message = document.querySelector("#edit-message").value;
  }

  fetch(fetch_url, {
    method: method,
    body: JSON.stringify({
      id_post: id_post,
      message: message,
    }),
  }).then(function (response) {
    if (response.status >= 200 && response.status < 300) {
      // Post message successfully
      // hide div with used to display error message
      document.querySelector("#error-message").innerHTML = "";
      document.querySelector("#error-message").style.display = "none";
      load_posts("", false, 0);
    } else {
      // Error was returned. Extract error content.
      response.json().then(function (data) {
        // Display error message.
        document.querySelector("#error-message").innerHTML = data.error;
        document.querySelector("#error-message").style.display = "block";
        window.scrollTo(0, 0);
        return false;
      });
    }
  });
  event.preventDefault();
}

//
// Function responsible to retrieve the posts and render it inside the DIV -> ID = #post-list
//
// Input username - it filters the post from the user, when blank return all posts.
//       follow_flag - if true, return posts from followed´s users
//
function load_posts(username, follow_flag, offset) {
  // define the url to be used to fectch the posts.
  if (follow_flag) {
    fetch_url = `/get_posts?f_flag=true&offset=${offset}`;
  } else {
    fetch_url = `/get_posts?username=${username}&offset=${offset}`;
  }

  fetch(fetch_url, {
    method: "GET",
  })
    .then(function (response) {
      if (response.status >= 200 && response.status < 300) {
        // Post message successfully
        // hide div with used to display error message
        document.querySelector("#error-message").innerHTML = "";
        document.querySelector("#error-message").style.display = "none";
        return response.json();
      } else {
        // Error was returned. Extract error content.
        response.json().then(function (data) {
          // Display error message.
          document.querySelector("#error-message").innerHTML = data.error;
          document.querySelector("#error-message").style.display = "block";
          window.scrollTo(0, 0);
          return false;
        });
      }
    })
    .then((posts) => {
      let html = "";

      for (post of posts["posts"]) {
        const {
          id,
          username,
          message,
          timestamp,
          likes,
          owner,
          owner_liked,
        } = post;

        // if logged user owns the post, so display edit buttin
        btn_edit = "";
        if (owner) {
          btn_edit = `<input type="button" class="btn btn-primary btn-sm" onclick="frm_post('#post-item-message-${id}',${id})" value="Edit">`;
        }

        // function returns the button like to be displayed
        const btn_liked = render_btn_liked(owner_liked, id);

        const line = `<div className="post-item">
          <div>
            <h6>
              <button type="button" onClick="load_profile('${username}')" class="btn btn-link">${username}</button>
            </h6>
          </div>         
          <div id="post-item-message-${id}">${message}</div>          
          <div>${timestamp}</div>
          <div>Likes:</div><div id="post-item-qtdlikes-${id}">${likes}</div>
          <div id="post-item-btnliked-${id}">${btn_liked}</div>
          <div id="post-item-btnedit-${id}">${btn_edit}</div>
        </div>
        <hr></hr>`;
        html = html + line;
      }

      // add the navigation bar
      html = html + html_page_navigation(posts["pages"], username, follow_flag);

      // displays DIV content required.
      document.querySelector("#post-list").innerHTML = html;
      document.querySelector("#post-list").style.display = "block";

      // Changes div all posts title, by adding the username when user is informed.
      if (follow_flag) {
        if (posts["posts"].length == 0) {
          document.querySelector("#title-allposts").innerHTML =
            "I can´t believe !!! Please, follow someone.";
        } else {
          document.querySelector("#title-allposts").innerHTML =
            "Posts from follow";
        }

        document.querySelector("#profile").style.display = "none";
        document.querySelector("#div-box-form").style.display = "none";
      } else if (username === "") {
        document.querySelector("#title-allposts").innerHTML = "All Posts";
        document.querySelector("#profile").style.display = "none";
        document.querySelector("#div-box-form").style.display = "block";
      } else {
        document.querySelector("#title-allposts").innerHTML =
          username + "'s posts";
      }
    });
  event.preventDefault();
}

//
// Function responsible to retrieve the profile and render it inside DIV -> ID = #profile
//
// Input username - it filters the post from the user, when blank return all posts.
//
function load_profile(username) {
  // Fetch user's profile data. user's name passed as GET parameter.
  fetch(`/get_profile?username=${username}`, {
    method: "GET",
  })
    .then(function (response) {
      if (response.status >= 200 && response.status < 300) {
        // Fetch successfuly processed, hide div for error message.
        document.querySelector("#error-message").innerHTML = "";
        document.querySelector("#error-message").style.display = "none";
        return response.json();
      } else {
        // Error was returned. Extract and display the error content.
        response.json().then(function (data) {
          // Display error message.
          document.querySelector("#error-message").innerHTML = data.error;
          document.querySelector("#error-message").style.display = "block";
          window.scrollTo(0, 0);
          return false;
        });
      }
    })
    .then((profile) => {
      let html = "";
      const {
        id,
        username,
        email,
        first_name,
        last_name,
        follows,
        followed,
        btnFollow,
      } = profile;
      let line = `<h1>${username}</h1>
      <div class="box">
        <div>
          <div class="row">
            <div class="col-1"><b>E-mail</b></div>
            <div class="col-11" id="email">${email}</div>
          </div>
          <div class="row">
            <div class="col-1"><b>First name</b></div>
            <div class="col-11" id="first_name">${first_name}</div>
          </div>
          <div class="row">
            <div class="col-1"><b>Last name</b></div>
            <div class="col-11" id="last_name">${last_name}</div>
          </div>
          <div class="row">
            <div class="col-1" id="follows"><span class="badge badge-pill badge-primary">Following ${follows}</span></div>
            <div class="col-1" id="followed"><span class="badge badge-pill badge-primary">Followers ${followed}</span></div>`;
      html = html + line;

      // Check if needs to hide follow or unfollow button
      if (btnFollow != "Hide") {
        line = `<div class="col-1" id="btn-follow">
            <button type="button" onClick="upd_follow(${id},'${username}','${btnFollow}')" class="btn btn-primary btn-sm">${btnFollow}</button>
        </div>`;
        html = html + line;
      }
      line = `
                        <div class="col-9" id="empty"></div>
          </div>
        </div>
      </div>`;
      html = html + line;

      // displays DIV content required for the profile page.
      document.querySelector("#profile").innerHTML = html;
      document.querySelector("#profile").style.display = "block";
      document.querySelector("#div-box-form").style.display = "none";

      // load user's posts and also display
      load_posts(username, false, 0); // load the user´s posts

      return false;
    });

  event.preventDefault();
}

//
//  Function responsible to implement follow and unfollow user.
//  Parameters - follow_id - user to be follower
//               follow_username - name of the user to be followed
//               oper - "Follows" or "Unfollow"
//
function upd_follow(follow_id, follow_username, oper) {
  // Invoke API to implement follow or unfollow
  fetch("/upd_follow", {
    method: "POST",
    body: JSON.stringify({
      user_followed: follow_id,
      oper: oper,
    }),
  }).then(function (response) {
    if (response.status >= 200 && response.status < 300) {
      // Post message successfully
      // hide div with used to display error message
      document.querySelector("#error-message").innerHTML = "";
      document.querySelector("#error-message").style.display = "none";
      load_profile(follow_username);
    } else {
      // Error was returned. Extract error content.
      response.json().then(function (data) {
        // Display error message.
        document.querySelector("#error-message").innerHTML = data.error;
        document.querySelector("#error-message").style.display = "block";
        window.scrollTo(0, 0);
        return false;
      });
    }
  });
  event.preventDefault();
}

//  Function responsible to update the like and unlike from a post.
//  Parameters - id_div - html tag where the quantity will be updated. Tag id format : '#post-item-liked-${id}'
//               id - post to be updated
//               oper - "liked" or "unliked"
//
function upd_like(id_post, oper) {
  // Invoke API to implement follow or unfollow
  fetch("/upd_like", {
    method: "PUT",
    body: JSON.stringify({
      id_post: id_post,
      oper: oper,
    }),
  }).then(function (response) {
    console.log("recebeu resposta do post");
    if (response.status >= 200 && response.status < 300) {
      // Post message successfully
      // hide div with used to display error message
      document.querySelector("#error-message").innerHTML = "";
      document.querySelector("#error-message").style.display = "none";

      // update the quantity.
      const qty_like_tag = `#post-item-qtdlikes-${id_post}`;
      qtd = parseInt(document.querySelector(qty_like_tag).innerHTML);
      if (oper === "liked") {
        qtd++;
        document.querySelector(qty_like_tag).innerHTML = qtd;
        owner_liked = true; // Owner liked, so flag to render the correct like button.
      } else {
        qtd--;
        document.querySelector(qty_like_tag).innerHTML = qtd;
        owner_liked = false; // Owner didn't like it, so flag to render the correct like button.
      }

      // html element where button needs to be refreshed
      const btn_like_tag = `#post-item-btnliked-${id_post}`;
      // call the function to get the button to be rendered.
      const btn_liked = render_btn_liked(owner_liked, id_post);
      // refresh the buttonh
      document.querySelector(btn_like_tag).innerHTML = btn_liked;
    } else {
      // Error was returned. Extract error content.
      response.json().then(function (data) {
        // Display error message.
        document.querySelector("#error-message").innerHTML = data.error;
        document.querySelector("#error-message").style.display = "block";
        window.scrollTo(0, 0);
        return false;
      });
    }
  });
  event.preventDefault();
}
*/
