<template>
    <div>
        <div class="col-md-10 ml-auto mr-auto">
            <div class="card" >
                <div class="card-body" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
                    <div class="col-lg-12">
                        <section class="Material-contact-section section-padding section-dark">
                            <div class="container">
                                <div class="row">
                                    <!-- Section Titile -->
                                    <div class="col-md-12 wow animated fadeInLeft" data-wow-delay=".2s">
                                        <h1 class="section-title">Love to Hear From You</h1>
                                    </div>
                                </div>
                                <div class="row">
                                    <!-- Section Titile -->
                                    <div class="col-md-6 mt-3 contact-widget-section2 wow animated fadeInLeft" data-wow-delay=".2s">
                                        <p>Pakistan's leading platform to deliver tailored solutions globally for every type of business process.</p>

                                        <div class="find-widget">
                                            <label>Company  :</label>   <a href="https://www.noblepos.com/"> Noble POS</a>
                                        </div>
                                        <div class="find-widget">
                                            <label>Address  :</label> <a href="javascript:void(0)"> Block # 3, 10B Ilyas Colony, Old Muslim Town Lahore, Pakistan</a>
                                        </div>
                                        <div class="find-widget">
                                            <label>UAN  :</label>   <a href="javascript:void(0)">+92 3111 444 592</a><br />
                                            <label>Headquarter :</label>  <a href="javascript:void(0)">+92 42 3575 0012</a><br />
                                            <label>Development Space  :</label>  <a href="javascript:void(0)">+92 42 3591 2979</a>
                                        </div>

                                        <div class="find-widget">
                                            <label>Website : </label>  <a href="http://www.noblepos.com/">www.noblepos.com/</a>
                                        </div>
                                        <div class="find-widget">
                                            <label>Program :</label>  <a href="javascript:void(0)">Mon to Sat: 10:00 AM - 8.00 PM</a>
                                        </div>
                                    </div>
                                    <!-- contact form -->
                                    <div class="col-md-6 wow animated fadeInRight" data-wow-delay=".2s">
                                       
                                            <!-- Name -->
                                            <div class="form-group label-floating">
                                                <label class="control-label" for="name">Name</label>
                                                <input class="form-control" id="name" type="text" name="name" required data-error="Please enter your name">
                                                <div class="help-block with-errors"></div>
                                            </div>
                                            <!-- email -->
                                            <div class="form-group label-floating">
                                                <label class="control-label" for="email">Email</label>
                                                <input class="form-control" id="email" type="email" name="email" required data-error="Please enter your Email">
                                                <div class="help-block with-errors"></div>
                                            </div>
                                            <!-- Subject -->
                                            <div class="form-group label-floating">
                                                <label class="control-label">Subject</label>
                                                <input class="form-control" id="msg_subject" type="text" name="subject" required data-error="Please enter your message subject">
                                                <div class="help-block with-errors"></div>
                                            </div>
                                            <!-- Message -->
                                            <div class="form-group label-floating">
                                                <label for="message" class="control-label">Message</label>
                                                <textarea class="form-control" rows="3" id="message" name="message" required data-error="Write your message"></textarea>
                                                <div class="help-block with-errors"></div>
                                            </div>
                                            <!-- Form Submit -->
                                            <div class="form-submit mt-5">
                                                <button class="btn btn-common" type="submit" id="form-submit"><i class="material-icons mdi mdi-message-outline"></i> Send Message</button>
                                                <div id="msgSubmit" class="h3 text-center hidden"></div>
                                                <div class="clearfix"></div>
                                            </div>
                                       
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>

    </div>
</template>
<script>
    export default {
        name: 'Support-component',
        data: function () {
            return {
                cvb: ''
            }
        },
        methods: {
        }
    }


</script>

