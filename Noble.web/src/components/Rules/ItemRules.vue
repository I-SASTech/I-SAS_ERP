<template>
    <div class="row" v-if="isValid('Can View Color')">
        <div class="col-lg-12 col-sm-12 ml-auto mr-auto">
            <div class="card ">
                <div class="card-header">
                    <h4 class="card-title DayHeading" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"> Items Rules</h4>

                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-lg-3 ">
                            <label class="ml-2">Same Item Name (English) :</label>
                        </div>
                        <div class="col-lg-3 ml-2">
                            <toggle-button v-model="rules.nameSame" class="ml-2" color="#3178F6"/>
                        </div>
                        <div class="col-lg-3 ">
                            <label class="ml-2">Same Item Name (Arabic) :</label>
                        </div>
                        <div class="col-lg-3 ">
                            <toggle-button v-model="rules.nameSame" class="ml-2" color="#3178F6"/>
                        </div>
                        <div class="col-lg-3">
                            <label class="ml-2">Image Required :</label>
                        </div>
                        <div class="col-lg-3">
                            <toggle-button v-model="rules.nameSame" class="ml-2" color="#3178F6"/>
                        </div>
                      

                    </div>
                   
                </div>
            </div>
        </div>


    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>

<script>
    import clickMixin from '@/Mixins/clickMixin'
    export default {
        mixins: [clickMixin],
        data: function () {
            return {
                arabic: '',
                english: '',
                rules:
                {
                    nameSame:false
                }

            }
        },
        methods: {
            IsSave: function () {


            },
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>