package controllers

import play.api.data._
import play.api.data.Forms._
import javax.inject._
import play.api.mvc._

case class PaymentForm(id: String, amount: Int, creditCard: String, callBackUrl: String)

@Singleton
class PayController @Inject()(cc: ControllerComponents) extends AbstractController(cc)     with play.api.i18n.I18nSupport {
  def index(id: String, amount: Int, callbackUrl: String): Action[AnyContent] = Action { implicit request: Request[AnyContent] =>
    Ok(views.html.payForm(userForm.fill(PaymentForm(id, amount, "", callbackUrl))))
  }
  
  val userForm = Form(
    mapping(
      "id" -> text,
      "amount" -> number,
      "creditCard" -> text,
      "callbackUrl"  -> text
    )(PaymentForm.apply)(PaymentForm.unapply)
  )

  val userPost: Action[PaymentForm] = Action(parse.form(userForm)) { implicit request =>
    val userData = request.body
    Redirect(userData.callBackUrl, Map("id" -> Seq(userData.id), "status" -> Seq("OK")), SEE_OTHER)
  }
}
