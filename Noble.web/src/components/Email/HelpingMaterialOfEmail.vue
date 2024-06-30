<template>
    <modal show="show" v-if="isValid('BasicMail') || isValid('StandardMail')">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel"> {{ $t('Helping Material') }}</h6>
                
                <button type="button" class="btn-close" v-on:click="close1()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                   
                </div>
            </div>
            <div class="modal-footer">
               
            </div>
        </div>



    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>


<script>
    export default {
        props: ['show1'],
        
        data: function () {
            return {
                data:''
            }
        },
        
        methods: {
            close1: function () {
                this.$emit('close1');
            },
            
           
        },
        created: function()
        {
          

        },
        mounted: function () {
           
        }
    }
</script>
