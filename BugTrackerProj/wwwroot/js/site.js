import { signalR } from "../microsoft/signalr/dist/browser/signalr"


(() => {
    var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44373/ApplicationHub", opts => {
        opts.Cookies.Add(new Cookie("ForceCookie", "NA", "/", url.DnsSafeHost));
    }).Build();
    connection.start();
    connection.on("loadBugs", function () {
        LoadBugData();
    })
    function LoadBugData() {
        var tr = ''
        $.ajax({
            url: ('https://localhost:44373/Home/Index'),
            method: 'Get',
            success: (result) => {
                $.each(result, (k, v) => {
                    tr += `<tr>
                        <td>
                            ${v.Category.CtaegoryName}
                        </td>
                        <td>
                            ${v.User.UserName}
                        </td>
                        <td>
                            ${v.BugDate}
                        </td>
                        <td>
                            ${v.Description}
                        </td>
                       </tr>`
                })
                $("#tablebody").html(tr);

                error: (error) =>
                    console.log(error);
            }


        }

        )
    }

})