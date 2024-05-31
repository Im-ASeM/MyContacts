using Microsoft.AspNetCore.Mvc;

public interface IContactController
{
    public IActionResult AddContact(NewContact newContact);
    public IActionResult ShowContact(int id);
    public IActionResult ShowContacts();
    public IActionResult ShowContactsId();
    public IActionResult DelContact(int id);
    public IActionResult UpContact(Contact newContact);
    public IActionResult ShowDatas();
}