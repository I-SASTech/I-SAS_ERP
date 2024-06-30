<template>
    <div class="row">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div class="card">
                <div class="card-header " style="padding-left:50px; padding-right:50px">
                    <h4 v-bind:class="$i18n.locale == 'ar' ? 'text-right' : ''"><strong>{{ $t('TermAndCondition.TermsCondition') }}</strong></h4>
                </div>
                <div class="card-body " style="padding-left:50px; padding-right:50px">
                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())">

                        By using the {{applicationName}} apps, these terms will
                        automatically apply to you.you should make sure therefore
                        that you read them carefully before using the application. You are
                        not allowed to copy, or modify the application, any part of the application,
                        or our trademarks in any way. You are not allowed to attempt
                        to extract the source code of the application, and you also
                        should not try to translate the application into other languages, or
                        make derivative versions. The application itself, and all the trade
                        marks, copyright, database rights and other intellectual
                        property rights related to it, still belong to
                        {{companyName}}.
                    </p>
                    <p v-else-if="($i18n.locale == 'pt')">

                        Ao usar os aplicativos {{applicationName}}, estes termos serão
                        aplicam-se automaticamente a você. você deve certificar-se, portanto,
                        que você os leia atentamente antes de usar o aplicativo. Você é
                        não é permitido copiar ou modificar o aplicativo, qualquer parte do aplicativo,
                        ou nossas marcas registradas de qualquer forma. Você não tem permissão para tentar
                        para extrair o código-fonte do aplicativo, e você também
                        não deve tentar traduzir o aplicativo para outros idiomas, ou
                        fazer versões derivadas. O aplicativo em si e todo o comércio
                        marcas, direitos autorais, direitos de banco de dados e outros direitos intelectuais
                        direitos de propriedade relacionados a ele, ainda pertencem
                        {{companyName}}.
                    </p>
                    <p v-else class="arabicLanguage">
                        باستخدام تطبيقات نوبل ، ستنطبق هذه الشروط عليك تلقائيًا ، لذا يجب عليك التأكد من قراءتها بعناية قبل استخدام التطبيق. لا يُسمح لك بنسخ أو تعديل التطبيق أو أي جزء من التطبيق أو علاماتنا التجارية بأي شكل من الأشكال. لا يُسمح لك بمحاولة استخراج الكود المصدري للتطبيق ، ويجب أيضًا ألا تحاول ترجمة التطبيق إلى لغات أخرى ، أو عمل إصدارات مشتقة. لا يزال التطبيق نفسه وجميع العلامات التجارية وحقوق النشر وحقوق قواعد البيانات وحقوق الملكية الفكرية الأخرى المتعلقة به مملوكة لشركة {{companyName}}. المحدودة.
                    </p>
                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())">
                        {{companyName}} is committed to ensuring that the application
                        is as useful and efficient as possible. For that reason, we
                        reserve the right to make changes to the application or to charge
                        for its services, at any time and for any reason. We will
                        never charge you for the application or its services without making
                        it very clear to you exactly what you are paying for.
                    </p>
                    <p v-else-if="($i18n.locale == 'pt')">
                        {{companyName}} está empenhada em garantir que a aplicação
                        é tão útil e eficiente quanto possível. Por essa razão, nós
                        reserva-se o direito de fazer alterações no aplicativo ou cobrar
                        por seus serviços, a qualquer momento e por qualquer motivo. Vamos
                        nunca cobrar pelo aplicativo ou seus serviços sem fazer
                        é muito claro para você exatamente o que você está pagando.
                    </p>
                    <p v-else class="arabicLanguage">
                        {{companyName}} الجندي. المحدودة ملتزمة بضمان أن يكون التطبيق مفيدًا وفعالًا قدر الإمكان. لهذا السبب ، نحتفظ بالحق في إجراء تغييرات على التطبيق أو فرض رسوم على خدماته في أي وقت ولأي سبب. لن نفرض عليك أي رسوم مقابل التطبيق أو خدماته دون أن نوضح لك بالضبط ما تدفعه مقابله.

                    </p>



                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())">
                        The {{applicationName}} apps stores and processes VAT, Employee data and shares this data with your parent
                        company who has created your account. It is your responsibility
                        to keep your username and password secure. Do not share your
                        credentials with anyone. By accepting these terms
                        and conditions, you also accept the use of required cookies. Our application
                        may need to collect your IP Address, your browser information for
                        security and fraud prevention.
                    </p>
                    <p v-else-if="($i18n.locale == 'pt')">
                        O {{applicationName}} aplicativo armazena e processa IVA, dados de funcionários e compartilha esses dados com seus pais
                        empresa que criou sua conta. É sua responsabilidade
                        para manter seu nome de usuário e senha seguros. Não compartilhe seu
                        credenciais com ninguém. Ao aceitar estes termos
                        e condições, você também aceita o uso dos cookies necessários. nosso aplicativo
                        pode precisar coletar seu endereço IP e as informações do seu navegador para
                        segurança e prevenção de fraudes.
                    </p>
                    <p v-else class="arabicLanguage">
                        تخزن تطبيقات نوبل وتعالج ضريبة القيمة المضافة وبيانات الموظفين وتشارك هذه البيانات مع الشركة الأم التي أنشأت حسابك. تقع على عاتقك مسؤولية الحفاظ على أمان اسم المستخدم وكلمة المرور الخاصين بك. لا تشارك بيانات الاعتماد الخاصة بك مع أي شخص. بقبولك لهذه الشروط والأحكام ، فإنك تقبل أيضًا استخدام ملفات تعريف الارتباط المطلوبة. قد يحتاج تطبيقنا إلى جمع عنوان IP الخاص بك ومعلومات متصفحك للأمان ومنع الاحتيال.
                    </p>
                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())">
                        You should be aware that there are certain things that
                        {{companyName}} will not take responsibility for.
                        To function, this application will require you to have an
                        active internet connection. The connection can be Wi-Fi, or
                        provided by your mobile network provider, but
                        {{companyName}} cannot take responsibility for the
                        application not working at full functionality if you do not have
                        access to Wi-Fi, and you do not have any of your data
                        allowance left.
                    </p>
                    <p v-else-if="($i18n.locale == 'pt' )">
                        Você deve estar ciente de que há certas coisas que
                        {{companyName}} não se responsabilizará por.
                        Para funcionar, este aplicativo exigirá que você tenha um
                        conexão de internet ativa. A conexão pode ser Wi-Fi ou
                        fornecido pelo seu provedor de rede móvel, mas
                        {{companyName}} não pode assumir a responsabilidade pelo
                        aplicativo não está funcionando com todas as funcionalidades se você não tiver
                        acesso ao Wi-Fi, e você não tem nenhum dos seus dados
                        subsídio deixado.
                    </p>
                    <p v-else class="arabicLanguage">
                        يجب أن تدرك أن هناك بعض الأشياء التي يقوم {{companyName}}. المحدودة لن تتحمل المسؤولية عن. لكي يعمل ، سيطلب منك هذا التطبيق أن يكون لديك اتصال إنترنت نشط. يمكن أن يكون الاتصال عبر Wi-Fi أو مقدمًا من مزود شبكة الهاتف المحمول الخاص بك ، ولكن {{companyName}}. لا تتحمل  المسؤولية عن التطبيق الذي لا يعمل بكامل وظائفه إذا لم يكن لديك وصول إلى شبكة Wi-Fi ، ولم يتبق لديك أي من البيانات المسموح بها.
                    </p>
                    <p></p>

                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())">
                        With respect to {{companyName}} responsibility for
                        your use of the application, when you are using the application, it is
                        important to bear in mind that although we endeavour to
                        ensure that it is updated and correct at all times, we do
                        rely on third parties to provide information to us so that
                        we can make it available to you.
                        {{companyName}} accepts no liability for any loss,
                        direct or indirect, you experience as a result of relying
                        wholly on this functionality of the application.
                    </p>
                    <p v-else-if="($i18n.locale == 'pt' )">
                        Com relação à responsabilidade da {{companyName}}
                        seu uso do aplicativo, quando você estiver usando o aplicativo, é
                        importante ter em mente que, embora nos esforcemos para
                        garantir que esteja atualizado e correto em todos os momentos, fazemos
                        confiar em terceiros para nos fornecer informações para que
                        podemos disponibilizá-lo para você.
                        {{companyName}} não aceita qualquer responsabilidade por qualquer perda,
                        direta ou indireta, você experimenta como resultado de confiar em
                        totalmente nesta funcionalidade do aplicativo.
                    </p>
                    <p v-else class="arabicLanguage">
                        فيما يتعلق بـ {{companyName}}. مسؤولية  عن استخدامك للتطبيق ، عند استخدام التطبيق ، من المهم أن تضع في اعتبارك أنه على الرغم من أننا نسعى لضمان تحديثه وصحته في جميع الأوقات ، فإننا نعتمد على أطراف ثالثة لتقديم المعلومات إلينا حتى نتمكن من إتاحته لك. {{companyName}} الجندي.  لا تتحمل أي مسؤولية عن أي خسارة ، مباشرة أو غير مباشرة ، تتعرض لها نتيجة الاعتماد كليًا على وظيفة التطبيق هذه.
                    </p>
                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())">
                        From time to time, we will be updating the application with
                        latest version to improve your experience. The application is
                        currently available on Web  the requirements for
                        system  may change,
                        and you will be informed of any changes we will be making to the
                        application. {{companyName}} does not promise that
                        it will always update the application. However, you promise to always
                        accept updates to the application when offered to you, We
                        may also wish to stop providing the application, and may terminate
                        use of it at any time without giving notice of termination
                        to you. Unless we tell you otherwise, upon any termination,
                        (a) the rights and licenses granted to you in these terms
                        will end; (b) you must stop using the application.
                    </p>
                    <p v-if="($i18n.locale == 'pt')">
                        Periodicamente, atualizaremos o aplicativo com
                        a versão mais recente para melhorar sua experiência. A aplicação é
                        atualmente disponíveis na Web os requisitos para
                        o sistema pode mudar,
                        e você será informado de quaisquer alterações que faremos no
                        aplicativo. {{companyName}} não promete isso
                        ele sempre atualizará o aplicativo. No entanto, você promete sempre
                        aceitar atualizações do aplicativo quando oferecidas a você, Nós
                        também pode desejar parar de fornecer o aplicativo e pode rescindir
                        usá-lo a qualquer momento sem aviso prévio de rescisão
                        para você. A menos que digamos o contrário, após qualquer rescisão,
                        (a) os direitos e licenças concedidos a você nestes termos
                        vai acabar; (b) você deve parar de usar o aplicativo.
                    </p>
                    <p v-else class="arabicLanguage">
                        من وقت لآخر ، سنقوم بتحديث التطبيق بأحدث إصدار لتحسين تجربتك. التطبيق متاح حاليًا على الويب وقد تتغير متطلبات النظام ، وسيتم إبلاغك بأي تغييرات سنجريها على التطبيق. {{companyName}} الجندي. لا تتعهد  بأنها ستقوم دائمًا بتحديث التطبيق. ومع ذلك ، فإنك تتعهد بقبول تحديثات التطبيق دائمًا عند عرضها عليك ، وقد نرغب أيضًا في التوقف عن تقديم التطبيق ، وقد ننهي استخدامه في أي وقت دون إشعار الإنهاء لك. ما لم نخبرك بخلاف ذلك ، عند أي إنهاء ، (أ) تنتهي الحقوق والتراخيص الممنوحة لك في هذه الشروط ؛ (ب) يجب عليك التوقف عن استخدام التطبيق.
                    </p>
                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())"><strong>Changes to these Terms and Conditions</strong></p>
                    <p v-else-if="($i18n.locale == 'pt' )"><strong>Alterações a estes Termos e Condições</strong></p>
                    <p v-else class="arabicLanguage"><strong>التغييرات على هذه الشروط والأحكام</strong></p>
                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())">
                        We may update our Terms and Conditions
                        from time to time. Whenever, we make a change, we will
                        notify you of any changes by posting the new Terms and
                        Conditions on this page. These changes are effective
                        immediately after they are posted on this page.
                    </p>
                    <p v-else-if="($i18n.locale == 'pt')">
                        Podemos atualizar nossos Termos e Condições
                        de tempos em tempos. Sempre que fizermos uma mudança, iremos
                        notificá-lo sobre quaisquer alterações, publicando os novos Termos e
                        Condições nesta página. Essas mudanças são eficazes
                        imediatamente após serem postados nesta página.
                    </p>
                    <p v-else class="arabicLanguage">
                        قد نقوم بتحديث الشروط والأحكام الخاصة بنا من وقت لآخر. عندما نقوم بإجراء تغيير ، سنخطرك بأي تغييرات عن طريق نشر الشروط والأحكام الجديدة على هذه الصفحة. تسري هذه التغييرات فور نشرها على هذه الصفحة.
                    </p>

                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())"><strong>Contact Us</strong></p>
                    <p v-else-if="($i18n.locale == 'pt' )"><strong>Contate-nos</strong></p>
                    <p v-else class="arabicLanguage">
                        <strong>
                            اتصل بنا
                        </strong>
                    </p>
                    <p v-if="($i18n.locale == 'en' ||isLeftToRight())">
                        If you have any questions or suggestions about
                        our Terms and Conditions, do not hesitate to
                        contact us at {{email}}
                    </p>
                    <p v-else-if="($i18n.locale == 'pt')">
                        Se você tiver alguma dúvida ou sugestão sobre
                        nossos Termos e Condições, não hesite em
                        contacte-nos em {{email}}
                    </p>
                    <p v-else class="arabicLanguage">

                        إذا كان لديك أي أسئلة أو اقتراحات حول الشروط والأحكام الخاصة بنا ، فلا تتردد في الاتصال بنا على {{email}}
                    </p>
                </div>
                <div class="card-footer text-center" v-if="!loading">
                    <button type="button" class="btn btn-primary  "  v-on:click="TermAndCondition"> {{ $t('TermAndCondition.IAgreeCondition') }}</button>
                </div>
                <div class="card-footer col-md-3" v-else>
                    <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
                </div>
            </div>
        </div>


    </div>


</template>
<script>
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        components: {
            Loading
        },

        data: function () {

            return {
                loading: false,
                companyId: '',
                companyName:'',
                email:'',
                applicationName:'',
                termsCondition: false
            }
        },

        methods: {


            TermAndCondition: function () {
                this.loading = true;
                if (this.termsCondition == false) {
                    var token = '';
                    var root = this;
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.loading = true;
                    root.$https.get('/Company/TermsAndConditionAgreed?companyId=' + root.companyId + '&termsCondition=' + true, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data == true) {

                                root.loading = false;

                                localStorage.setItem('TermsCondition', true);
                                root.$router.push('/Setup');
                                root.$router.go();
                            }

                        });


                }
                else {
                    this.$router.push('/Setup');
                }


            },

        },
        mounted: function () {


            var terms = localStorage.getItem('TermsCondition');
            this.companyId = localStorage.getItem('CompanyID');
            this.companyName = localStorage.getItem('CompanyName')
            this.applicationName = localStorage.getItem('ApplicationName')
            this.email = localStorage.getItem('email')

            if (terms == 'true') {
                this.termsCondition = true;
            }

        }
    }

</script>