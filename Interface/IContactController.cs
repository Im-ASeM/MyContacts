using Microsoft.AspNetCore.Mvc;

public interface IContactController
{
    public IActionResult AddContact(MMyContacts newContact);
    public IActionResult ShowContact(int id);
    public IActionResult ShowContacts();
    public IActionResult DelContact(int id);
    public IActionResult UpContact(int id , MMyContacts newContact);
    public IActionResult ShowDatas();
}